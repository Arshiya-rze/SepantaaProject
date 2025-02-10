using api.Helpers;

namespace api.Repositories;

public class MemberRepository : IMemberRepository
{
    #region Constructor
    IMongoCollection<AppUser>? _collectionAppUser;
    IMongoCollection<Attendence>? _collectionAttendence;
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;

    public MemberRepository(IMongoClient client, IMyMongoDbSettings dbSettings, ITokenService tokenService, UserManager<AppUser> userManager)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collectionAppUser = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);
        _collectionAttendence = database.GetCollection<Attendence>(AppVariablesExtensions.collectionAttendences);

        _tokenService = tokenService;
        _userManager = userManager;
    }
    #endregion Constructor

    // public async Task<Attendence[]> FindByUserIdAsync(AttendenceParams attendenceParams, CancellationToken cancellationToken)
    // {
    //     Attendence attendence[] = await _collectionAttendence.Find<Attendence>(doc
    //         => doc.StudentId == attendenceParams.UserId).ToList(cancellationToken);

    //     if (appUser is null) return null;

    //     return appUser;
    // }

    // public async Task<PagedList<Attendence>> GetAllAttendenceAsync(AttendenceParams attendenceParams, CancellationToken cancellationToken)
    // {
    //     // PagedList<Attendence> attendences = _collectionAttendence.Find<Attendence>(doc
    //     // => doc.StudentId == attendenceParams.UserId).ToList(cancellationToken);

    //     IMongoQueryable<Attendence>? query = _collectionAttendence.AsQueryable<Attendence>()
    //         .Where(doc => doc.StudentId == attendenceParams.UserId);
        
    //     return await PagedList<Attendence>.CreatePagedListAsync(query, attendenceParams.PageNumber, attendenceParams.PageSize, cancellationToken);
    // }

    public async Task<bool?> UpdateMemberAsync(MemberUpdateDto memberUpdateDto, string? hashedUserId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(hashedUserId)) return false;

        ObjectId? userId = await _tokenService.GetActualUserIdAsync(hashedUserId, cancellationToken);
        if (userId == null) return false;

        AppUser? targetAppUser = await _userManager.FindByIdAsync(userId.ToString());
        if (targetAppUser == null) return false;

        targetAppUser.Email = memberUpdateDto.Email;
        targetAppUser.UserName = memberUpdateDto.UserName;

        if (!string.IsNullOrEmpty(memberUpdateDto.currentPassword) && 
            !string.IsNullOrEmpty(memberUpdateDto.Password) && 
            !string.IsNullOrEmpty(memberUpdateDto.ConfirmPassword))
        {
            //Change password if last password exist
            IdentityResult passwordChangeResult = await _userManager.ChangePasswordAsync(targetAppUser, memberUpdateDto.currentPassword, memberUpdateDto.Password);
            if (!passwordChangeResult.Succeeded)
            {
                var errors = string.Join(", ", passwordChangeResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to change password: {errors}");
            }
        }

        targetAppUser.NormalizedEmail = memberUpdateDto.Email.ToUpper();
        targetAppUser.NormalizedUserName = memberUpdateDto.UserName.ToUpper();
        
        //save changes in DataBase
        IdentityResult updateResult = await _userManager.UpdateAsync(targetAppUser);
        return updateResult.Succeeded;
        
        // if (string.IsNullOrEmpty(hashedUserId)) return null;

        // ObjectId? userId = await _tokenService.GetActualUserIdAsync(hashedUserId, cancellationToken);

        // if (userId is null) return null;

        // UpdateDefinition<AppUser> updatedMember = Builders<AppUser>.Update
        // .Set(appUser => appUser.Email, memberUpdateDto.Email)
        // .Set(appUser => appUser.UserName, memberUpdateDto.UserName)
        // .Set(appUser => appUser.PasswordHash, memberUpdateDto.Password)
        // .Set(appUser => appUser.PasswordHash, memberUpdateDto.ConfirmPassword);

        // return await _collectionAppUser.UpdateOneAsync<AppUser>(appUser => appUser.Id == userId, updatedMember, null, cancellationToken);
    }

    public async Task<ProfileDto> GetProfileAsync(string HashedUserId, CancellationToken cancellationToken)
    {
        //tabdil userId be ObjectId        
        ObjectId? userId = await _tokenService.GetActualUserIdAsync(HashedUserId, cancellationToken);

        if (userId is null) return null;

        AppUser appUser = await _collectionAppUser.Find<AppUser>(appUser => appUser.Id == userId).
            FirstOrDefaultAsync(cancellationToken);

        return appUser is null
            ? null
            : Mappers.ConvertAppUserToProfileDto(appUser);

    }

    public async Task<MemberDto?> GetByUserNameAsync(string memberUserName, CancellationToken cancellationToken)
    {
        // ObjectId memberId = await _collectionAppUser.AsQueryable()
        // .Where(appUser => appUser.NormalizedUserName == memberUserName)
        // .Select(appUser => appUser.Id)
        // .FirstOrDefaultAsync(cancellationToken);

        AppUser appUser = await _collectionAppUser.Find<AppUser>(appUser =>
                appUser.NormalizedUserName == memberUserName).FirstOrDefaultAsync(cancellationToken);

        if (appUser.ToString() is not null)
        {
            return Mappers.ConvertAppUserToMemberDto(appUser);
        }

        return null;
    }

    public async Task<List<AppUser>> GetAllClassmateAsync(string userIdHashed, CancellationToken cancellationToken)
    {
        ObjectId? userId = await _tokenService.GetActualUserIdAsync(userIdHashed, cancellationToken);

        if (userId is null) return null;

        AppUser loggedInAppUser = await _collectionAppUser.Find<AppUser>(doc =>
            doc.Id ==  userId).FirstOrDefaultAsync(cancellationToken);
        
        if (loggedInAppUser is null)
            return null;

        List<string> targetCourseIds = loggedInAppUser.EnrolledCourses.Select(course => course.CourseId.ToString()).ToList();


        List<AppUser> classmates = await _collectionAppUser.AsQueryable<AppUser>()
            .Where(appUser => appUser.EnrolledCourses.Any(course => targetCourseIds.Contains(course.CourseId)) && appUser.Id != userId)
            .ToListAsync(cancellationToken);

        // List<AppUser>? targetAppUsers = _collectionAppUser.Find<AppUser>(
        //     doc => doc.EnrolledCourses.Course == targetAppUser.EnrolledCourses.CourseId).ToList(cancellationToken);

        if (classmates is null)
            return null;

        return classmates;
    }
}