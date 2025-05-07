import { createRequire } from 'module';const require = createRequire(import.meta.url);
import {
  require_dist
} from "./chunk-PJR6N5F2.js";
import {
  FormControlDirective,
  FormControlName
} from "./chunk-BU2IDENW.js";
import {
  CommonModule,
  DecimalPipe,
  NgForOf,
  NgIf
} from "./chunk-423NJ3L7.js";
import {
  Component,
  ContentChild,
  Directive,
  ElementRef,
  EventEmitter,
  HostListener,
  Input,
  NgModule,
  Output,
  Pipe,
  setClassMetadata,
  ɵɵadvance,
  ɵɵclassProp,
  ɵɵcontentQuery,
  ɵɵdefineComponent,
  ɵɵdefineDirective,
  ɵɵdefineInjector,
  ɵɵdefineNgModule,
  ɵɵdefinePipe,
  ɵɵdirectiveInject,
  ɵɵelement,
  ɵɵelementContainerEnd,
  ɵɵelementContainerStart,
  ɵɵelementEnd,
  ɵɵelementStart,
  ɵɵgetCurrentView,
  ɵɵlistener,
  ɵɵloadQuery,
  ɵɵnamespaceHTML,
  ɵɵnamespaceSVG,
  ɵɵnextContext,
  ɵɵpipe,
  ɵɵpipeBind2,
  ɵɵprojection,
  ɵɵprojectionDef,
  ɵɵproperty,
  ɵɵpureFunction0,
  ɵɵqueryRefresh,
  ɵɵreference,
  ɵɵresetView,
  ɵɵresolveDocument,
  ɵɵrestoreView,
  ɵɵstyleProp,
  ɵɵtemplate,
  ɵɵtext,
  ɵɵtextInterpolate
} from "./chunk-LMIDRGFX.js";
import "./chunk-2H3L6IVL.js";
import {
  __spreadValues,
  __toESM
} from "./chunk-NQ4HTGF6.js";

// node_modules/ng-persian-datepicker/fesm2022/ng-persian-datepicker.mjs
var import_jalali_ts = __toESM(require_dist(), 1);
var _c0 = ["*"];
var _c1 = () => [];
function NgPersianDatepickerComponent_div_17_div_2_Template(rf, ctx) {
  if (rf & 1) {
    const _r2 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 31);
    ɵɵlistener("click", function NgPersianDatepickerComponent_div_17_div_2_Template_div_click_0_listener() {
      const year_r3 = ɵɵrestoreView(_r2).$implicit;
      const ctx_r3 = ɵɵnextContext(2);
      return ɵɵresetView(ctx_r3.yearClick(year_r3));
    });
    ɵɵelementStart(1, "div", 32, 3)(3, "span");
    ɵɵtext(4);
    ɵɵelementEnd()()();
  }
  if (rf & 2) {
    const year_r3 = ctx.$implicit;
    const yearCol_r5 = ɵɵreference(2);
    const ctx_r3 = ɵɵnextContext(2);
    ɵɵadvance();
    ɵɵstyleProp("background-color", year_r3.isYearDisabled ? ctx_r3.uiTheme.disabledBackground : year_r3.isYearOfSelectedDate ? ctx_r3.uiTheme.selectedBackground : yearCol_r5.classList.contains("hover") ? ctx_r3.uiTheme.hoverBackground : year_r3.isYearOfTodayDate ? ctx_r3.uiTheme.todayBackground : null)("color", year_r3.isYearDisabled ? ctx_r3.uiTheme.disabledText : year_r3.isYearOfSelectedDate ? ctx_r3.uiTheme.selectedText : yearCol_r5.classList.contains("hover") ? ctx_r3.uiTheme.hoverText : year_r3.isYearOfTodayDate ? ctx_r3.uiTheme.todayText : null);
    ɵɵclassProp("disabled", year_r3.isYearDisabled)("selected", year_r3.isYearOfSelectedDate)("today", year_r3.isYearOfTodayDate);
    ɵɵadvance(3);
    ɵɵtextInterpolate(year_r3.value);
  }
}
function NgPersianDatepickerComponent_div_17_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 28)(1, "div", 29);
    ɵɵtemplate(2, NgPersianDatepickerComponent_div_17_div_2_Template, 5, 11, "div", 30);
    ɵɵelementEnd()();
  }
  if (rf & 2) {
    const ctx_r3 = ɵɵnextContext();
    ɵɵadvance();
    ɵɵclassProp("no-margin-bottom", !ctx_r3.timeEnable && !ctx_r3.uiTodayBtnEnable);
    ɵɵadvance();
    ɵɵproperty("ngForOf", ctx_r3.years);
  }
}
function NgPersianDatepickerComponent_div_18_div_2_Template(rf, ctx) {
  if (rf & 1) {
    const _r6 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 35);
    ɵɵlistener("click", function NgPersianDatepickerComponent_div_18_div_2_Template_div_click_0_listener() {
      const month_r7 = ɵɵrestoreView(_r6).$implicit;
      const ctx_r3 = ɵɵnextContext(2);
      return ɵɵresetView(ctx_r3.monthClick(month_r7));
    });
    ɵɵelementStart(1, "div", 32, 4)(3, "span");
    ɵɵtext(4);
    ɵɵpipe(5, "month");
    ɵɵelementEnd()()();
  }
  if (rf & 2) {
    const month_r7 = ctx.$implicit;
    const monthCol_r8 = ɵɵreference(2);
    const ctx_r3 = ɵɵnextContext(2);
    ɵɵadvance();
    ɵɵstyleProp("background-color", month_r7.isMonthDisabled ? ctx_r3.uiTheme.disabledBackground : month_r7.isMonthOfSelectedDate ? ctx_r3.uiTheme.selectedBackground : monthCol_r8.classList.contains("hover") ? ctx_r3.uiTheme.hoverBackground : month_r7.isMonthOfTodayDate ? ctx_r3.uiTheme.todayBackground : null)("color", month_r7.isMonthDisabled ? ctx_r3.uiTheme.disabledText : month_r7.isMonthOfSelectedDate ? ctx_r3.uiTheme.selectedText : monthCol_r8.classList.contains("hover") ? ctx_r3.uiTheme.hoverText : month_r7.isMonthOfTodayDate ? ctx_r3.uiTheme.todayText : null);
    ɵɵclassProp("disabled", month_r7.isMonthDisabled)("selected", month_r7.isMonthOfSelectedDate)("today", month_r7.isMonthOfTodayDate);
    ɵɵadvance(3);
    ɵɵtextInterpolate(ɵɵpipeBind2(5, 11, month_r7.indexValue, ctx_r3.calendarIsGregorian));
  }
}
function NgPersianDatepickerComponent_div_18_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 33)(1, "div", 29);
    ɵɵtemplate(2, NgPersianDatepickerComponent_div_18_div_2_Template, 6, 14, "div", 34);
    ɵɵelementEnd()();
  }
  if (rf & 2) {
    const ctx_r3 = ɵɵnextContext();
    ɵɵadvance();
    ɵɵclassProp("no-margin-bottom", !ctx_r3.timeEnable && !ctx_r3.uiTodayBtnEnable);
    ɵɵadvance();
    ɵɵproperty("ngForOf", ctx_r3.months);
  }
}
function NgPersianDatepickerComponent_div_19_div_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 41)(1, "span");
    ɵɵtext(2);
    ɵɵelementEnd()();
  }
  if (rf & 2) {
    const weekDay_r9 = ctx.$implicit;
    ɵɵadvance(2);
    ɵɵtextInterpolate(weekDay_r9);
  }
}
function NgPersianDatepickerComponent_div_19_ng_container_4_div_1_Template(rf, ctx) {
  if (rf & 1) {
    const _r10 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 43);
    ɵɵlistener("click", function NgPersianDatepickerComponent_div_19_ng_container_4_div_1_Template_div_click_0_listener() {
      const day_r11 = ɵɵrestoreView(_r10).$implicit;
      const ctx_r3 = ɵɵnextContext(3);
      return ɵɵresetView(ctx_r3.dayClick(day_r11));
    });
    ɵɵelementStart(1, "div", 32, 5)(3, "span");
    ɵɵtext(4);
    ɵɵelementEnd()()();
  }
  if (rf & 2) {
    const day_r11 = ctx.$implicit;
    const dayCol_r12 = ɵɵreference(2);
    const ctx_r3 = ɵɵnextContext(3);
    ɵɵadvance();
    ɵɵstyleProp("background-color", day_r11.isDayDisabled ? ctx_r3.uiTheme.disabledBackground : day_r11.isDayOfSelectedDate ? ctx_r3.uiTheme.selectedBackground : dayCol_r12.classList.contains("hover") ? ctx_r3.uiTheme.hoverBackground : day_r11.isDayOfTodayDate ? ctx_r3.uiTheme.todayBackground : !day_r11.isDayInCurrentMonth ? ctx_r3.uiTheme.otherMonthBackground : null)("color", day_r11.isDayDisabled ? ctx_r3.uiTheme.disabledText : day_r11.isDayOfSelectedDate ? ctx_r3.uiTheme.selectedText : dayCol_r12.classList.contains("hover") ? ctx_r3.uiTheme.hoverText : day_r11.isDayOfTodayDate ? ctx_r3.uiTheme.todayText : !day_r11.isDayInCurrentMonth ? ctx_r3.uiTheme.otherMonthText : null);
    ɵɵclassProp("disabled", day_r11.isDayDisabled)("selected", day_r11.isDayOfSelectedDate)("today", day_r11.isDayOfTodayDate)("other-month", !day_r11.isDayInCurrentMonth);
    ɵɵadvance(3);
    ɵɵtextInterpolate(day_r11.value);
  }
}
function NgPersianDatepickerComponent_div_19_ng_container_4_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementContainerStart(0);
    ɵɵtemplate(1, NgPersianDatepickerComponent_div_19_ng_container_4_div_1_Template, 5, 13, "div", 42);
    ɵɵelementContainerEnd();
  }
  if (rf & 2) {
    const row_r13 = ctx.$implicit;
    ɵɵadvance();
    ɵɵproperty("ngForOf", row_r13);
  }
}
function NgPersianDatepickerComponent_div_19_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 36)(1, "div", 37);
    ɵɵtemplate(2, NgPersianDatepickerComponent_div_19_div_2_Template, 3, 1, "div", 38);
    ɵɵelementEnd();
    ɵɵelementStart(3, "div", 39);
    ɵɵtemplate(4, NgPersianDatepickerComponent_div_19_ng_container_4_Template, 2, 1, "ng-container", 40);
    ɵɵelementEnd()();
  }
  if (rf & 2) {
    const ctx_r3 = ɵɵnextContext();
    ɵɵadvance(2);
    ɵɵproperty("ngForOf", ctx_r3.weekDays);
    ɵɵadvance();
    ɵɵclassProp("no-margin-bottom", !ctx_r3.timeEnable && !ctx_r3.uiTodayBtnEnable);
    ɵɵadvance();
    ɵɵproperty("ngForOf", ctx_r3.days);
  }
}
function NgPersianDatepickerComponent_ng_container_20_ng_container_7_div_1_Template(rf, ctx) {
  if (rf & 1) {
    const _r14 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 46)(1, "div", 52, 6);
    ɵɵlistener("click", function NgPersianDatepickerComponent_ng_container_20_ng_container_7_div_1_Template_div_click_1_listener() {
      const i_r15 = ɵɵrestoreView(_r14).index;
      const ctx_r3 = ɵɵnextContext(3);
      return ɵɵresetView(ctx_r3.setHour(i_r15));
    });
    ɵɵelementStart(3, "span");
    ɵɵtext(4);
    ɵɵpipe(5, "number");
    ɵɵelementEnd()()();
  }
  if (rf & 2) {
    const i_r15 = ctx.index;
    const hourCol_r16 = ɵɵreference(2);
    const ctx_r3 = ɵɵnextContext(3);
    ɵɵadvance();
    ɵɵstyleProp("background-color", ctx_r3.hour === i_r15 ? ctx_r3.uiTheme.selectedBackground : hourCol_r16.classList.contains("hover") ? ctx_r3.uiTheme.hoverBackground : null)("color", ctx_r3.hour === i_r15 ? ctx_r3.uiTheme.selectedText : hourCol_r16.classList.contains("hover") ? ctx_r3.uiTheme.hoverText : null);
    ɵɵclassProp("selected", ctx_r3.hour === i_r15);
    ɵɵadvance(3);
    ɵɵtextInterpolate(ɵɵpipeBind2(5, 7, i_r15, "2.0"));
  }
}
function NgPersianDatepickerComponent_ng_container_20_ng_container_7_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementContainerStart(0);
    ɵɵtemplate(1, NgPersianDatepickerComponent_ng_container_20_ng_container_7_div_1_Template, 6, 10, "div", 49);
    ɵɵelementContainerEnd();
  }
  if (rf & 2) {
    ɵɵadvance();
    ɵɵproperty("ngForOf", ɵɵpureFunction0(1, _c1).constructor(24));
  }
}
function NgPersianDatepickerComponent_ng_container_20_ng_container_8_div_1_Template(rf, ctx) {
  if (rf & 1) {
    const _r17 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 46)(1, "div", 52, 7);
    ɵɵlistener("click", function NgPersianDatepickerComponent_ng_container_20_ng_container_8_div_1_Template_div_click_1_listener() {
      const i_r18 = ɵɵrestoreView(_r17).index;
      const ctx_r3 = ɵɵnextContext(3);
      return ɵɵresetView(ctx_r3.set12Hour(i_r18 + 1));
    });
    ɵɵelementStart(3, "span");
    ɵɵtext(4);
    ɵɵpipe(5, "number");
    ɵɵelementEnd()()();
  }
  if (rf & 2) {
    const i_r18 = ctx.index;
    const hour12Col_r19 = ɵɵreference(2);
    const ctx_r3 = ɵɵnextContext(3);
    ɵɵadvance();
    ɵɵstyleProp("background-color", ctx_r3.hour === 0 && i_r18 + 1 === 12 || ctx_r3.hour >= 1 && ctx_r3.hour <= 12 && ctx_r3.hour === i_r18 + 1 || ctx_r3.hour > 12 && ctx_r3.hour === i_r18 + 1 + 12 ? ctx_r3.uiTheme.selectedBackground : hour12Col_r19.classList.contains("hover") ? ctx_r3.uiTheme.hoverBackground : null)("color", ctx_r3.hour === 0 && i_r18 + 1 === 12 || ctx_r3.hour >= 1 && ctx_r3.hour <= 12 && ctx_r3.hour === i_r18 + 1 || ctx_r3.hour > 12 && ctx_r3.hour === i_r18 + 1 + 12 ? ctx_r3.uiTheme.selectedText : hour12Col_r19.classList.contains("hover") ? ctx_r3.uiTheme.hoverText : null);
    ɵɵclassProp("selected", ctx_r3.hour === 0 && i_r18 + 1 === 12 || ctx_r3.hour >= 1 && ctx_r3.hour <= 12 && ctx_r3.hour === i_r18 + 1 || ctx_r3.hour > 12 && ctx_r3.hour === i_r18 + 1 + 12);
    ɵɵadvance(3);
    ɵɵtextInterpolate(ɵɵpipeBind2(5, 7, i_r18 + 1, "2.0"));
  }
}
function NgPersianDatepickerComponent_ng_container_20_ng_container_8_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementContainerStart(0);
    ɵɵtemplate(1, NgPersianDatepickerComponent_ng_container_20_ng_container_8_div_1_Template, 6, 10, "div", 49);
    ɵɵelementContainerEnd();
  }
  if (rf & 2) {
    ɵɵadvance();
    ɵɵproperty("ngForOf", ɵɵpureFunction0(1, _c1).constructor(12));
  }
}
function NgPersianDatepickerComponent_ng_container_20_div_18_Template(rf, ctx) {
  if (rf & 1) {
    const _r20 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 46)(1, "div", 52, 8);
    ɵɵlistener("click", function NgPersianDatepickerComponent_ng_container_20_div_18_Template_div_click_1_listener() {
      const i_r21 = ɵɵrestoreView(_r20).index;
      const ctx_r3 = ɵɵnextContext(2);
      return ɵɵresetView(ctx_r3.setMinute(i_r21));
    });
    ɵɵelementStart(3, "span");
    ɵɵtext(4);
    ɵɵpipe(5, "number");
    ɵɵelementEnd()()();
  }
  if (rf & 2) {
    const i_r21 = ctx.index;
    const minuteCol_r22 = ɵɵreference(2);
    const ctx_r3 = ɵɵnextContext(2);
    ɵɵadvance();
    ɵɵstyleProp("background-color", ctx_r3.minute === i_r21 ? ctx_r3.uiTheme.selectedBackground : minuteCol_r22.classList.contains("hover") ? ctx_r3.uiTheme.hoverBackground : null)("color", ctx_r3.minute === i_r21 ? ctx_r3.uiTheme.selectedText : minuteCol_r22.classList.contains("hover") ? ctx_r3.uiTheme.hoverText : null);
    ɵɵclassProp("selected", ctx_r3.minute === i_r21);
    ɵɵadvance(3);
    ɵɵtextInterpolate(ɵɵpipeBind2(5, 7, i_r21, "2.0"));
  }
}
function NgPersianDatepickerComponent_ng_container_20_div_23_div_5_Template(rf, ctx) {
  if (rf & 1) {
    const _r23 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 46)(1, "div", 52, 9);
    ɵɵlistener("click", function NgPersianDatepickerComponent_ng_container_20_div_23_div_5_Template_div_click_1_listener() {
      const i_r24 = ɵɵrestoreView(_r23).index;
      const ctx_r3 = ɵɵnextContext(3);
      return ɵɵresetView(ctx_r3.setSecond(i_r24));
    });
    ɵɵelementStart(3, "span");
    ɵɵtext(4);
    ɵɵpipe(5, "number");
    ɵɵelementEnd()()();
  }
  if (rf & 2) {
    const i_r24 = ctx.index;
    const secondCol_r25 = ɵɵreference(2);
    const ctx_r3 = ɵɵnextContext(3);
    ɵɵadvance();
    ɵɵstyleProp("background-color", ctx_r3.second === i_r24 ? ctx_r3.uiTheme.selectedBackground : secondCol_r25.classList.contains("hover") ? ctx_r3.uiTheme.hoverBackground : null)("color", ctx_r3.second === i_r24 ? ctx_r3.uiTheme.selectedText : secondCol_r25.classList.contains("hover") ? ctx_r3.uiTheme.hoverText : null);
    ɵɵclassProp("selected", ctx_r3.second === i_r24);
    ɵɵadvance(3);
    ɵɵtextInterpolate(ɵɵpipeBind2(5, 7, i_r24, "2.0"));
  }
}
function NgPersianDatepickerComponent_ng_container_20_div_23_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 53)(1, "div", 46)(2, "div", 47)(3, "span");
    ɵɵtext(4);
    ɵɵelementEnd()()();
    ɵɵtemplate(5, NgPersianDatepickerComponent_ng_container_20_div_23_div_5_Template, 6, 10, "div", 49);
    ɵɵelementStart(6, "div", 46)(7, "div", 47)(8, "span");
    ɵɵtext(9);
    ɵɵelementEnd()()()();
  }
  if (rf & 2) {
    const ctx_r3 = ɵɵnextContext(2);
    ɵɵadvance();
    ɵɵstyleProp("border-bottom-color", ctx_r3.uiTheme.border);
    ɵɵadvance(3);
    ɵɵtextInterpolate(ctx_r3.second);
    ɵɵadvance();
    ɵɵproperty("ngForOf", ɵɵpureFunction0(7, _c1).constructor(60));
    ɵɵadvance();
    ɵɵstyleProp("border-top-color", ctx_r3.uiTheme.border);
    ɵɵadvance(3);
    ɵɵtextInterpolate(ctx_r3.second);
  }
}
function NgPersianDatepickerComponent_ng_container_20_div_24_Template(rf, ctx) {
  if (rf & 1) {
    const _r26 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 54)(1, "div", 46)(2, "div", 52, 10);
    ɵɵlistener("click", function NgPersianDatepickerComponent_ng_container_20_div_24_Template_div_click_2_listener() {
      ɵɵrestoreView(_r26);
      const ctx_r3 = ɵɵnextContext(2);
      return ɵɵresetView(ctx_r3.toggleAmPm("AM"));
    });
    ɵɵelementStart(4, "span");
    ɵɵtext(5, "AM");
    ɵɵelementEnd()()();
    ɵɵelementStart(6, "div", 46)(7, "div", 52, 11);
    ɵɵlistener("click", function NgPersianDatepickerComponent_ng_container_20_div_24_Template_div_click_7_listener() {
      ɵɵrestoreView(_r26);
      const ctx_r3 = ɵɵnextContext(2);
      return ɵɵresetView(ctx_r3.toggleAmPm("PM"));
    });
    ɵɵelementStart(9, "span");
    ɵɵtext(10, "PM");
    ɵɵelementEnd()()()();
  }
  if (rf & 2) {
    const amCol_r27 = ɵɵreference(3);
    const pmCol_r28 = ɵɵreference(8);
    const ctx_r3 = ɵɵnextContext(2);
    ɵɵadvance(2);
    ɵɵstyleProp("background-color", ctx_r3.hour < 12 ? ctx_r3.uiTheme.selectedBackground : amCol_r27.classList.contains("hover") ? ctx_r3.uiTheme.hoverBackground : null)("color", ctx_r3.hour < 12 ? ctx_r3.uiTheme.selectedText : amCol_r27.classList.contains("hover") ? ctx_r3.uiTheme.hoverText : null);
    ɵɵclassProp("selected", ctx_r3.hour < 12);
    ɵɵadvance(5);
    ɵɵstyleProp("background-color", ctx_r3.hour >= 12 ? ctx_r3.uiTheme.selectedBackground : pmCol_r28.classList.contains("hover") ? ctx_r3.uiTheme.hoverBackground : null)("color", ctx_r3.hour >= 12 ? ctx_r3.uiTheme.selectedText : pmCol_r28.classList.contains("hover") ? ctx_r3.uiTheme.hoverText : null);
    ɵɵclassProp("selected", ctx_r3.hour >= 12);
  }
}
function NgPersianDatepickerComponent_ng_container_20_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementContainerStart(0);
    ɵɵelementStart(1, "div", 44)(2, "div", 45)(3, "div", 46)(4, "div", 47)(5, "span");
    ɵɵtext(6);
    ɵɵelementEnd()()();
    ɵɵtemplate(7, NgPersianDatepickerComponent_ng_container_20_ng_container_7_Template, 2, 2, "ng-container", 26)(8, NgPersianDatepickerComponent_ng_container_20_ng_container_8_Template, 2, 2, "ng-container", 26);
    ɵɵelementStart(9, "div", 46)(10, "div", 47)(11, "span");
    ɵɵtext(12);
    ɵɵelementEnd()()()();
    ɵɵelementStart(13, "div", 48)(14, "div", 46)(15, "div", 47)(16, "span");
    ɵɵtext(17);
    ɵɵelementEnd()()();
    ɵɵtemplate(18, NgPersianDatepickerComponent_ng_container_20_div_18_Template, 6, 10, "div", 49);
    ɵɵelementStart(19, "div", 46)(20, "div", 47)(21, "span");
    ɵɵtext(22);
    ɵɵelementEnd()()()();
    ɵɵtemplate(23, NgPersianDatepickerComponent_ng_container_20_div_23_Template, 10, 8, "div", 50)(24, NgPersianDatepickerComponent_ng_container_20_div_24_Template, 11, 12, "div", 51);
    ɵɵelementEnd();
    ɵɵelementContainerEnd();
  }
  if (rf & 2) {
    const ctx_r3 = ɵɵnextContext();
    ɵɵadvance();
    ɵɵstyleProp("border-top-color", ctx_r3.uiTheme.timeBorder)("border-bottom-color", ctx_r3.uiTheme.timeBorder);
    ɵɵadvance(2);
    ɵɵstyleProp("border-bottom-color", ctx_r3.uiTheme.border);
    ɵɵadvance(3);
    ɵɵtextInterpolate(ctx_r3.hour);
    ɵɵadvance();
    ɵɵproperty("ngIf", !ctx_r3.timeMeridian);
    ɵɵadvance();
    ɵɵproperty("ngIf", ctx_r3.timeMeridian);
    ɵɵadvance();
    ɵɵstyleProp("border-top-color", ctx_r3.uiTheme.border);
    ɵɵadvance(3);
    ɵɵtextInterpolate(ctx_r3.hour);
    ɵɵadvance(2);
    ɵɵstyleProp("border-bottom-color", ctx_r3.uiTheme.border);
    ɵɵadvance(3);
    ɵɵtextInterpolate(ctx_r3.minute);
    ɵɵadvance();
    ɵɵproperty("ngForOf", ɵɵpureFunction0(21, _c1).constructor(60));
    ɵɵadvance();
    ɵɵstyleProp("border-top-color", ctx_r3.uiTheme.border);
    ɵɵadvance(3);
    ɵɵtextInterpolate(ctx_r3.minute);
    ɵɵadvance();
    ɵɵproperty("ngIf", ctx_r3.timeShowSecond);
    ɵɵadvance();
    ɵɵproperty("ngIf", ctx_r3.timeMeridian);
  }
}
function NgPersianDatepickerComponent_ng_container_22_Template(rf, ctx) {
  if (rf & 1) {
    const _r29 = ɵɵgetCurrentView();
    ɵɵelementContainerStart(0);
    ɵɵelementStart(1, "div", 55, 12);
    ɵɵlistener("click", function NgPersianDatepickerComponent_ng_container_22_Template_div_click_1_listener() {
      ɵɵrestoreView(_r29);
      const ctx_r3 = ɵɵnextContext();
      return ɵɵresetView(ctx_r3.selectToday());
    });
    ɵɵelementStart(3, "span");
    ɵɵtext(4);
    ɵɵelementEnd()();
    ɵɵelementContainerEnd();
  }
  if (rf & 2) {
    const today_r30 = ɵɵreference(2);
    const ctx_r3 = ɵɵnextContext();
    ɵɵadvance();
    ɵɵstyleProp("background-color", today_r30.classList.contains("hover") ? ctx_r3.uiTheme.hoverBackground : null)("color", today_r30.classList.contains("hover") ? ctx_r3.uiTheme.hoverText : null);
    ɵɵadvance(3);
    ɵɵtextInterpolate(ctx_r3.calendarIsGregorian ? "Today" : "امروز");
  }
}
var defaultTheme = {
  border: "#CCCCCC",
  timeBorder: "#CCCCCC",
  background: "#FFFFFF",
  text: "#333333",
  hoverBackground: "#007BE6",
  hoverText: "#FFFFFF",
  disabledBackground: "#F1F1F1",
  disabledText: "#CCCCCC",
  selectedBackground: "#005299",
  selectedText: "#FFFFFF",
  todayBackground: "#333333",
  todayText: "#FFFFFF",
  otherMonthBackground: "rgba(0, 0, 0, 0)",
  otherMonthText: "#CCCCCC"
};
var faWeekDays = ["ش", "ی", "د", "س", "چ", "پ", "ج"];
var enWeekDays = ["S", "M", "T", "W", "T", "F", "S"];
var faMonths = ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"];
var enMonths = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
var ThemeHoverDirective = class _ThemeHoverDirective {
  elementRef;
  constructor(elementRef) {
    this.elementRef = elementRef;
  }
  onMouseOver() {
    this.elementRef.nativeElement?.classList?.add("hover");
  }
  onMouseOut() {
    this.elementRef.nativeElement?.classList?.remove("hover");
  }
  static ɵfac = function ThemeHoverDirective_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _ThemeHoverDirective)(ɵɵdirectiveInject(ElementRef));
  };
  static ɵdir = ɵɵdefineDirective({
    type: _ThemeHoverDirective,
    selectors: [["", "themeHover", ""]],
    hostBindings: function ThemeHoverDirective_HostBindings(rf, ctx) {
      if (rf & 1) {
        ɵɵlistener("mouseover", function ThemeHoverDirective_mouseover_HostBindingHandler() {
          return ctx.onMouseOver();
        })("mouseout", function ThemeHoverDirective_mouseout_HostBindingHandler() {
          return ctx.onMouseOut();
        });
      }
    }
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(ThemeHoverDirective, [{
    type: Directive,
    args: [{
      selector: "[themeHover]",
      standalone: false
    }]
  }], () => [{
    type: ElementRef
  }], {
    onMouseOver: [{
      type: HostListener,
      args: ["mouseover"]
    }],
    onMouseOut: [{
      type: HostListener,
      args: ["mouseout"]
    }]
  });
})();
var MonthPipe = class _MonthPipe {
  transform(index, gregorian) {
    return gregorian ? enMonths[index] : faMonths[index];
  }
  static ɵfac = function MonthPipe_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _MonthPipe)();
  };
  static ɵpipe = ɵɵdefinePipe({
    name: "month",
    type: _MonthPipe,
    pure: true
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(MonthPipe, [{
    type: Pipe,
    args: [{
      name: "month",
      standalone: false
    }]
  }], null, null);
})();
var NgPersianDatepickerComponent = class _NgPersianDatepickerComponent {
  elementRef;
  constructor(elementRef) {
    this.elementRef = elementRef;
    this.setToday();
  }
  input;
  inputEventFocusListener;
  formControl;
  formControlValueChanges;
  dateValue;
  lastEmittedDateValue;
  preventClose = false;
  uiYearView = true;
  uiMonthView = true;
  today;
  viewDate;
  selectedDate;
  wasInsideClick = false;
  viewDateTitle = "";
  viewModes = [];
  viewModeIndex = 0;
  weekDays = faWeekDays;
  years = [];
  months = [];
  days = [];
  hour = 0;
  minute = 0;
  second = 0;
  /** @ReactiveForm */
  set _formControlDirective(value) {
    this.setFormControl(value?.control);
  }
  set _formControlName(value) {
    this.setFormControl(value?.control);
  }
  /** @Input */
  // calendar
  calendarIsGregorian = false;
  set _calendarIsGregorian(value) {
    this.weekDays = value ? enWeekDays : faWeekDays;
    this.calendarIsGregorian = value;
  }
  // date
  inputDateValue;
  dateInitValue = true;
  dateIsGregorian = false;
  dateFormat = "YYYY/MM/DD";
  set _dateFormat(value) {
    this.dateFormat = value.replace(new RegExp("j", "g"), "");
  }
  dateGregorianFormat = "YYYY-MM-DD";
  dateMin = null;
  set _dateMin(value) {
    this.dateMin = value;
    if (this.days.length) this.setViewDate();
  }
  dateMax = null;
  set _dateMax(value) {
    this.dateMax = value;
    if (this.days.length) this.setViewDate();
  }
  // time
  timeEnable = false;
  set _timeEnable(value) {
    this.timeEnable = value;
    if (!this.timeEnable && this.dateValueDefined()) this.onChangeSelectedDate(true);
    this.setTime();
  }
  timeShowSecond = false;
  set _timeShowSecond(value) {
    this.timeShowSecond = value;
  }
  timeMeridian = false;
  set _timeMeridian(value) {
    this.timeMeridian = value;
  }
  // ui
  uiTheme = defaultTheme;
  set _uiTheme(value) {
    this.uiTheme = __spreadValues(__spreadValues({}, defaultTheme), value);
  }
  uiIsVisible = false;
  uiHideOnOutsideClick = true;
  uiHideAfterSelectDate = true;
  set _uiYearView(value) {
    this.uiYearView = value;
    this.checkViewModes();
    this.setViewDateTitle();
  }
  set _uiMonthView(value) {
    this.uiMonthView = value;
    this.checkViewModes();
    this.setViewDateTitle();
  }
  uiInitViewMode = "day";
  uiTodayBtnEnable = true;
  /** @Output */
  // date
  dateOnInit = new EventEmitter();
  dateOnSelect = new EventEmitter();
  // ui
  uiIsVisibleChange = new EventEmitter();
  ngOnInit() {
    this.setViewModes();
    this.setInitViewMode();
    this.setShowOnInputFocus();
    if (this.inputDateValue) this.setFormControl(this.inputDateValue);
  }
  ngOnDestroy() {
    this.formControlValueChanges?.unsubscribe();
    if (this.input) {
      this.input.removeEventListener("focus", this.inputEventFocusListener);
    }
  }
  dateValueDefined() {
    return typeof this.dateValue === "number";
  }
  setFormControl(value) {
    if (!value) return;
    this.formControl = value;
    if (!this.dateValueDefined()) {
      this.setDateInitValue(this.formControl?.value);
      this.setSelectedDate(this.formControl?.value);
      this.setViewDate();
      this.setTime();
      this.setFormControlValue();
    }
    this.formControlValueChanges?.unsubscribe();
    this.formControlValueChanges = this.formControl?.valueChanges?.subscribe({
      next: (value2) => {
        if (typeof value2 === "string" && !value2.trim() || typeof value2 === "number" && Number.isNaN(value2) || value2 === null || value2 === void 0) {
          this.dateValue = void 0;
          this.lastEmittedDateValue = void 0;
          this.selectedDate = void 0;
          this.setViewDate();
          return;
        }
        let valueOf = void 0;
        try {
          valueOf = this.valueOfDate(value2);
        } catch (e) {
          return;
        }
        if (typeof valueOf === "undefined" || valueOf === this.dateValue) {
          return;
        }
        const date = import_jalali_ts.Jalali.timestamp(valueOf, false);
        if (!this.isDateInRange(date.valueOf(), false, false)) {
          return;
        }
        this.setTime(date);
        this.changeSelectedDate(date, false);
      }
    });
  }
  setToday() {
    const today = import_jalali_ts.Jalali.now(false);
    if (!this.timeEnable) today.startOf("day");
    this.today = today;
  }
  setViewModes() {
    this.viewModes = ["day"];
    if (this.uiMonthView) {
      this.viewModes.push("month");
    }
    if (this.uiYearView) {
      this.viewModes.push("year");
    }
    if (this.viewModes.length <= this.viewModeIndex) {
      this.viewModeIndex = 0;
    }
  }
  setInitViewMode() {
    const index = this.viewModes.indexOf(this.uiInitViewMode);
    if (index !== -1) this.viewModeIndex = index;
  }
  checkViewModes() {
    let viewModesCount = 1;
    if (this.uiYearView) {
      viewModesCount++;
    }
    if (this.uiMonthView) {
      viewModesCount++;
    }
    if (viewModesCount !== this.viewModes.length) {
      this.setViewModes();
    }
  }
  setDateInitValue(dateValue) {
    if (dateValue || !this.dateInitValue) {
      return;
    }
    this.dateValue = this.today.valueOf();
    this.selectedDate = import_jalali_ts.Jalali.timestamp(this.dateValue, false);
    this.lastEmittedDateValue = +this.selectedDate;
    this.dateOnInit.next({
      shamsi: String(this.selectedDate.format(this.dateFormat)),
      gregorian: String(this.selectedDate.gregorian(this.dateGregorianFormat)),
      timestamp: Number(this.selectedDate.valueOf())
    });
  }
  setSelectedDate(dateValue) {
    if (!dateValue) {
      return;
    }
    const date = import_jalali_ts.Jalali.timestamp(this.valueOfDate(dateValue), false);
    if (!this.timeEnable) date.startOf("day");
    this.dateValue = date.valueOf();
    this.selectedDate = date;
  }
  setViewDate() {
    if (!this.dateValueDefined()) {
      this.viewDate = this.dateMax ? import_jalali_ts.Jalali.timestamp(this.dateMax, false) : this.today.clone();
    } else {
      this.viewDate = this.dateMax && this.selectedDate.valueOf() > this.dateMax.valueOf() ? import_jalali_ts.Jalali.timestamp(this.dateMax, false) : this.selectedDate.clone();
    }
    if (!this.timeEnable) this.viewDate.startOf("day");
    this.onChangeViewDate();
  }
  onChangeViewDate() {
    if (this.calendarIsGregorian) {
      this.viewDate.date.setDate(1);
    } else {
      this.viewDate.startOf("month");
    }
    this.setYears();
    this.setMonths();
    this.setDays();
    this.setViewDateTitle();
  }
  setYears() {
    this.years = [];
    const clone = this.viewDate.clone();
    const years = this.calendarIsGregorian ? clone.date : clone;
    if (years instanceof Date) {
      years.setDate(1);
      years.setMonth(0);
      years.setFullYear(years.getFullYear() - 6);
    } else {
      years.startOf("year");
      years.add(-6, "year");
    }
    for (let i = 0; i < 12; i++) {
      const year = [+years, years.getFullYear()];
      this.years.push({
        timestamp: year[0],
        value: year[1],
        isYearOfTodayDate: this.isYearOfTodayDate(year),
        isYearOfSelectedDate: this.isYearOfSelectedDate(year),
        isYearDisabled: this.isYearDisabled(year)
      });
      if (years instanceof Date) {
        years.setFullYear(years.getFullYear() + 1);
      } else {
        years.add(1, "year");
      }
    }
  }
  setMonths() {
    this.months = [];
    const clone = this.viewDate.clone();
    const months = this.calendarIsGregorian ? clone.date : clone;
    if (months instanceof Date) {
      months.setDate(1);
      months.setMonth(0);
    } else {
      months.startOf("year");
    }
    for (let i = 0; i < 12; i++) {
      const month = [+months, months.getFullYear(), months.getMonth()];
      this.months.push({
        timestamp: month[0],
        year: month[1],
        indexValue: month[2],
        isMonthOfTodayDate: this.isMonthOfToday(month),
        isMonthOfSelectedDate: this.isMonthOfSelectedDate(month),
        isMonthDisabled: this.isMonthDisabled(month)
      });
      if (months instanceof Date) {
        months.setMonth(months.getMonth() + 1);
      } else {
        months.add(1, "month");
      }
    }
  }
  setDays() {
    this.days = [];
    const prevMonthDetails = [];
    const currentMonthDetails = [];
    const nextMonthDetails = [];
    const prevMonth = import_jalali_ts.Jalali.timestamp(+this.viewDate, false);
    const currentMonth = import_jalali_ts.Jalali.timestamp(+this.viewDate, false);
    const nextMonth = import_jalali_ts.Jalali.timestamp(+this.viewDate, false);
    if (this.calendarIsGregorian) {
      prevMonth.date.setMonth(prevMonth.date.getMonth() - 1);
      nextMonth.date.setMonth(nextMonth.date.getMonth() + 1);
    } else {
      prevMonth.add(-1, "month");
      nextMonth.add(1, "month");
    }
    const gregorianMonthDays = (date) => new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
    const currentMonthDays = this.calendarIsGregorian ? gregorianMonthDays(currentMonth.date) : currentMonth.monthLength();
    const prevMonthDays = this.calendarIsGregorian ? gregorianMonthDays(prevMonth.date) : prevMonth.monthLength();
    const nextMonthDays = this.calendarIsGregorian ? gregorianMonthDays(nextMonth.date) : nextMonth.monthLength();
    for (let i = 0; i < prevMonthDays; i++) {
      if (this.calendarIsGregorian) {
        prevMonthDetails.push([+prevMonth.date, prevMonth.date.getFullYear(), prevMonth.date.getMonth(), prevMonth.date.getDate()]);
      } else {
        prevMonthDetails.push([+prevMonth, prevMonth.getFullYear(), prevMonth.getMonth(), prevMonth.getDate()]);
      }
      prevMonth.add(1, "day");
    }
    for (let i = 0; i < currentMonthDays; i++) {
      if (this.calendarIsGregorian) {
        currentMonthDetails.push([+currentMonth, currentMonth.date.getFullYear(), currentMonth.date.getMonth(), currentMonth.date.getDate()]);
      } else {
        currentMonthDetails.push([+currentMonth, currentMonth.getFullYear(), currentMonth.getMonth(), currentMonth.getDate()]);
      }
      currentMonth.add(1, "day");
    }
    for (let i = 0; i < nextMonthDays; i++) {
      if (this.calendarIsGregorian) {
        nextMonthDetails.push([+nextMonth, nextMonth.date.getFullYear(), nextMonth.date.getMonth(), nextMonth.date.getDate()]);
      } else {
        nextMonthDetails.push([+nextMonth, nextMonth.getFullYear(), nextMonth.getMonth(), nextMonth.getDate()]);
      }
      nextMonth.add(1, "day");
    }
    for (let row = 0; row < 6; row++) {
      const rowValue = [];
      for (let col = 0; col < 7; col++) {
        const fromPrevMonth = this.calendarIsGregorian ? this.viewDate.date.getDay() : this.viewDate.date.getDay() === 6 ? 0 : this.viewDate.date.getDay() + 1;
        let index = row * 7 + col - fromPrevMonth;
        let day = [];
        if (index < 0) {
          index = prevMonthDetails.length - (fromPrevMonth - col);
          day = prevMonthDetails[index];
        } else if (index >= currentMonthDetails.length) {
          index = index - currentMonthDetails.length;
          day = nextMonthDetails[index];
        } else {
          day = currentMonthDetails[index];
        }
        rowValue.push({
          timestamp: day[0],
          year: day[1],
          monthIndex: day[2],
          value: day[3],
          isDayInCurrentMonth: this.isDayInCurrentMonth(day),
          isDayOfTodayDate: this.isDayOfTodayDate(day),
          isDayOfSelectedDate: this.isDayOfSelectedDate(day),
          isDayDisabled: this.isDayDisabled(day)
        });
      }
      this.days.push(rowValue);
    }
  }
  setViewDateTitle() {
    if (!this.viewDate) {
      return;
    }
    const date = this.calendarIsGregorian ? this.viewDate.date : this.viewDate;
    const year = date.getFullYear();
    switch (this.viewModes[this.viewModeIndex]) {
      case "day":
        this.viewDateTitle = `${this.calendarIsGregorian ? enMonths[date.getMonth()] : faMonths[date.getMonth()]} ${year}`;
        break;
      case "month":
        this.viewDateTitle = year.toString();
        break;
      case "year":
        this.viewDateTitle = (year - 6).toString() + "-" + (year + 5).toString();
        break;
    }
  }
  setTime(date = null) {
    if (date) {
      this.hour = date.getHours();
      this.minute = date.getMinutes();
      this.second = date.getSeconds();
    } else if (this.selectedDate) {
      this.hour = this.selectedDate.getHours();
      this.minute = this.selectedDate.getMinutes();
      this.second = this.selectedDate.getSeconds();
    } else {
      this.hour = this.today.getHours();
      this.minute = this.today.getMinutes();
      this.second = this.today.getSeconds();
    }
  }
  setFormControlValue() {
    if (!this.formControl) {
      return;
    }
    if (this.dateValueDefined()) {
      this.formControl?.setValue(import_jalali_ts.Jalali.timestamp(this.dateValue, false).format(this.dateFormat, this.calendarIsGregorian));
    }
  }
  setShowOnInputFocus() {
    const input = this.elementRef.nativeElement?.querySelector("input");
    if (!input) {
      return;
    }
    this.inputEventFocusListener = () => {
      if (!this.uiIsVisible) {
        this.setUiIsVisible(true);
      }
    };
    this.input = input;
    this.input.addEventListener("focus", this.inputEventFocusListener);
  }
  skipViewDate(skip, type) {
    if (type === 1) {
      this.calendarIsGregorian ? this.viewDate.date.setFullYear(this.viewDate.date.getFullYear() + skip) : this.viewDate.add(skip, "year");
    } else if (type === 2) {
      this.calendarIsGregorian ? this.viewDate.date.setMonth(this.viewDate.date.getMonth() + skip) : this.viewDate.add(skip, "month");
    }
  }
  navigate(forward) {
    let skip = 1;
    if (!forward) {
      skip = skip * -1;
    }
    switch (this.viewModes[this.viewModeIndex]) {
      case "day":
        this.skipViewDate(skip, 2);
        break;
      case "month":
        this.skipViewDate(skip, 1);
        break;
      case "year":
        this.skipViewDate(skip * 12, 1);
        break;
    }
    this.onChangeViewDate();
  }
  nextViewMode() {
    if (this.viewModes.length === 1) {
      return;
    }
    if (this.viewModes.length <= this.viewModeIndex + 1) {
      this.viewModeIndex = 0;
    } else {
      this.viewModeIndex++;
    }
    this.setViewDateTitle();
  }
  selectToday() {
    this.setToday();
    this.preventClose = true;
    this.changeSelectedDate(this.today);
  }
  yearClick(year) {
    if (year.isYearDisabled) {
      return;
    }
    this.viewDate = import_jalali_ts.Jalali.timestamp(year.timestamp, false);
    let viewModeIndex = this.viewModes.indexOf("month");
    if (viewModeIndex === -1) {
      viewModeIndex = this.viewModes.indexOf("day");
    }
    this.viewModeIndex = viewModeIndex;
    this.onChangeViewDate();
  }
  monthClick(month) {
    if (month.isMonthDisabled) {
      return;
    }
    this.viewDate = import_jalali_ts.Jalali.timestamp(month.timestamp, false);
    this.viewModeIndex = this.viewModes.indexOf("day");
    this.onChangeViewDate();
  }
  dayClick(day) {
    if (day.isDayDisabled) {
      return;
    }
    this.changeSelectedDate(import_jalali_ts.Jalali.timestamp(day.timestamp, false));
  }
  isYearOfTodayDate(year) {
    const date = this.calendarIsGregorian ? this.today.date : this.today;
    return year[1] === date.getFullYear();
  }
  isYearOfSelectedDate(year) {
    if (!this.selectedDate) {
      return false;
    }
    const date = this.calendarIsGregorian ? this.selectedDate.date : this.selectedDate;
    return year[1] === date.getFullYear();
  }
  isYearDisabled(month) {
    return !this.isDateInRange(month[0], true, false);
  }
  isMonthOfToday(month) {
    const date = this.calendarIsGregorian ? this.today.date : this.today;
    return month[1] === date.getFullYear() && month[2] === date.getMonth();
  }
  isMonthOfSelectedDate(month) {
    if (!this.selectedDate) {
      return false;
    }
    const date = this.calendarIsGregorian ? this.selectedDate.date : this.selectedDate;
    return month[1] === date.getFullYear() && month[2] === date.getMonth();
  }
  isMonthDisabled(month) {
    return !this.isDateInRange(month[0], false, true);
  }
  isDayInCurrentMonth(day) {
    const date = this.calendarIsGregorian ? this.viewDate.date : this.viewDate;
    return day[1] === date.getFullYear() && day[2] === date.getMonth();
  }
  isDayOfTodayDate(day) {
    const date = this.calendarIsGregorian ? this.today.date : this.today;
    return day[1] === date.getFullYear() && day[2] === date.getMonth() && day[3] === date.getDate();
  }
  isDayOfSelectedDate(day) {
    if (!this.selectedDate) {
      return false;
    }
    const date = this.calendarIsGregorian ? this.selectedDate.date : this.selectedDate;
    return day[1] === date.getFullYear() && day[2] === date.getMonth() && day[3] === date.getDate();
  }
  isDayDisabled(day) {
    return !this.isDateInRange(day[0], false, false);
  }
  isDateInRange(date, isYear, isMonth) {
    const result = [];
    if (this.dateMin) {
      const min = import_jalali_ts.Jalali.timestamp(this.dateMin, false);
      if (isYear) {
        if (this.calendarIsGregorian) {
          min.date.setDate(1);
          min.date.setMonth(0);
        } else {
          min.startOf("year");
        }
      }
      if (isMonth) {
        if (this.calendarIsGregorian) {
          min.date.setDate(1);
        } else {
          min.startOf("month");
        }
      }
      result.push(min.valueOf() <= date);
    }
    if (this.dateMax) {
      const max = import_jalali_ts.Jalali.timestamp(this.dateMax, false);
      if (isYear) {
        if (this.calendarIsGregorian) {
          max.date.setDate(1);
          max.date.setMonth(0);
        } else {
          max.startOf("year");
        }
      }
      if (isMonth) {
        if (this.calendarIsGregorian) {
          max.date.setDate(1);
        } else {
          max.startOf("month");
        }
      }
      result.push(max.valueOf() >= date);
    }
    return !(result.indexOf(false) !== -1);
  }
  changeSelectedDate(date, setInputValue = true) {
    this.selectedDate = date.clone();
    this.onChangeSelectedDate(setInputValue);
  }
  onChangeSelectedDate(setInputValue) {
    if (this.timeEnable) {
      this.selectedDate.setHours(this.hour);
      this.selectedDate.setMinutes(this.minute);
      this.selectedDate.setSeconds(this.second);
      this.selectedDate.setMilliseconds(0);
    } else {
      this.selectedDate.startOf("day");
    }
    this.dateValue = this.selectedDate.valueOf();
    if (this.uiHideAfterSelectDate && !this.preventClose) {
      this.setUiIsVisible(false);
    } else {
      this.preventClose = false;
    }
    if (this.lastEmittedDateValue === +this.selectedDate) return;
    if (setInputValue) {
      this.setFormControlValue();
    }
    this.setViewDate();
    this.lastEmittedDateValue = +this.selectedDate;
    this.dateOnSelect.next({
      shamsi: String(this.selectedDate.format(this.dateFormat)),
      gregorian: String(this.selectedDate.gregorian(this.dateGregorianFormat)),
      timestamp: Number(this.selectedDate.valueOf())
    });
  }
  set12Hour(value) {
    let hour = value;
    const isAM = this.hour < 12;
    const isPM = this.hour >= 12;
    if (isAM && hour === 12) {
      hour = 0;
    }
    if (isPM && hour === 12) {
      hour = 12;
    }
    if (isPM && hour < 12) {
      hour = value + 12;
    }
    this.setHour(hour);
  }
  setHour(value) {
    if (value === this.hour) {
      return;
    }
    this.hour = value;
    this.onTimeChange();
  }
  setMinute(value) {
    if (value === this.minute) {
      return;
    }
    this.minute = value;
    this.onTimeChange();
  }
  setSecond(value) {
    if (value === this.second) {
      return;
    }
    this.second = value;
    this.onTimeChange();
  }
  toggleAmPm(current) {
    if (current === "AM" && this.hour < 12 || current === "PM" && this.hour >= 12) {
      return;
    }
    if (this.hour < 12) {
      this.hour += 12;
    } else {
      this.hour -= 12;
    }
    this.onTimeChange();
  }
  onTimeChange() {
    this.preventClose = true;
    if (!this.selectedDate) this.selectedDate = this.today.clone();
    this.changeSelectedDate(this.selectedDate);
  }
  onInsideClick() {
    this.wasInsideClick = true;
  }
  onOutsideClick() {
    const wasInsideClick = Boolean(this.wasInsideClick);
    this.wasInsideClick = false;
    if (wasInsideClick || !this.uiHideOnOutsideClick) {
      return;
    }
    this.setUiIsVisible(false);
  }
  valueOfDate(date) {
    if (typeof date === "string") {
      const gregorian = this.calendarIsGregorian || this.dateIsGregorian && !this.dateValueDefined();
      return gregorian ? +import_jalali_ts.Jalali.gregorian(date, false) : +import_jalali_ts.Jalali.parse(date, false);
    }
    return date;
  }
  setUiIsVisible(value) {
    this.uiIsVisible = value;
    this.uiIsVisibleChange.next(value);
  }
  static ɵfac = function NgPersianDatepickerComponent_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NgPersianDatepickerComponent)(ɵɵdirectiveInject(ElementRef));
  };
  static ɵcmp = ɵɵdefineComponent({
    type: _NgPersianDatepickerComponent,
    selectors: [["ng-persian-datepicker"]],
    contentQueries: function NgPersianDatepickerComponent_ContentQueries(rf, ctx, dirIndex) {
      if (rf & 1) {
        ɵɵcontentQuery(dirIndex, FormControlDirective, 5);
        ɵɵcontentQuery(dirIndex, FormControlName, 5);
      }
      if (rf & 2) {
        let _t;
        ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx._formControlDirective = _t.first);
        ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx._formControlName = _t.first);
      }
    },
    hostBindings: function NgPersianDatepickerComponent_HostBindings(rf, ctx) {
      if (rf & 1) {
        ɵɵlistener("click", function NgPersianDatepickerComponent_click_HostBindingHandler() {
          return ctx.onInsideClick();
        })("click", function NgPersianDatepickerComponent_click_HostBindingHandler() {
          return ctx.onOutsideClick();
        }, false, ɵɵresolveDocument);
      }
    },
    inputs: {
      _calendarIsGregorian: [0, "calendarIsGregorian", "_calendarIsGregorian"],
      inputDateValue: [0, "dateValue", "inputDateValue"],
      dateInitValue: "dateInitValue",
      dateIsGregorian: "dateIsGregorian",
      _dateFormat: [0, "dateFormat", "_dateFormat"],
      dateGregorianFormat: "dateGregorianFormat",
      _dateMin: [0, "dateMin", "_dateMin"],
      _dateMax: [0, "dateMax", "_dateMax"],
      _timeEnable: [0, "timeEnable", "_timeEnable"],
      _timeShowSecond: [0, "timeShowSecond", "_timeShowSecond"],
      _timeMeridian: [0, "timeMeridian", "_timeMeridian"],
      _uiTheme: [0, "uiTheme", "_uiTheme"],
      uiIsVisible: "uiIsVisible",
      uiHideOnOutsideClick: "uiHideOnOutsideClick",
      uiHideAfterSelectDate: "uiHideAfterSelectDate",
      _uiYearView: [0, "uiYearView", "_uiYearView"],
      _uiMonthView: [0, "uiMonthView", "_uiMonthView"],
      uiInitViewMode: "uiInitViewMode",
      uiTodayBtnEnable: "uiTodayBtnEnable"
    },
    outputs: {
      dateOnInit: "dateOnInit",
      dateOnSelect: "dateOnSelect",
      uiIsVisibleChange: "uiIsVisibleChange"
    },
    ngContentSelectors: _c0,
    decls: 23,
    vars: 27,
    consts: [["goBack", ""], ["switchView", ""], ["goForward", ""], ["yearCol", ""], ["monthCol", ""], ["dayCol", ""], ["hourCol", ""], ["hour12Col", ""], ["minuteCol", ""], ["secondCol", ""], ["amCol", ""], ["pmCol", ""], ["today", ""], [1, "datepicker-content"], [1, "datepicker-outer-container", 3, "dir"], [1, "datepicker-inner-container"], [1, "content-container", "navigation-container", "horizontal-padding"], ["themeHover", "", 1, "go-back", "dp-btn", 3, "click"], ["xmlns", "http://www.w3.org/2000/svg", "viewBox", "0 0 240.823 240.823"], ["d", "M183.189,111.816L74.892,3.555c-4.752-4.74-12.451-4.74-17.215,0c-4.752,4.74-4.752,12.439,0,17.179\n                   l99.707,99.671l-99.695,99.671c-4.752,4.74-4.752,12.439,0,17.191c4.752,4.74,12.463,4.74,17.215,0l108.297-108.261\n                   C187.881,124.315,187.881,116.495,183.189,111.816z"], ["themeHover", "", 1, "switch-view", "dp-btn", 3, "click"], ["themeHover", "", 1, "go-forward", "dp-btn", 3, "click"], ["d", "M57.633,129.007L165.93,237.268c4.752,4.74,12.451,4.74,17.215,0c4.752-4.74,4.752-12.439,0-17.179\n                   l-99.707-99.671l99.695-99.671c4.752-4.74,4.752-12.439,0-17.191c-4.752-4.74-12.463-4.74-17.215,0L57.621,111.816\n                   C52.942,116.507,52.942,124.327,57.633,129.007z"], ["class", "years-container horizontal-padding", 4, "ngIf"], ["class", "months-container horizontal-padding", 4, "ngIf"], ["class", "days-container horizontal-padding", 4, "ngIf"], [4, "ngIf"], [1, "the-toolbox", "horizontal-padding"], [1, "years-container", "horizontal-padding"], [1, "content-container"], ["class", "year-col three-col-per-row", 3, "click", 4, "ngFor", "ngForOf"], [1, "year-col", "three-col-per-row", 3, "click"], ["themeHover", "", 1, "dp-btn"], [1, "months-container", "horizontal-padding"], ["class", "month-col three-col-per-row", 3, "click", 4, "ngFor", "ngForOf"], [1, "month-col", "three-col-per-row", 3, "click"], [1, "days-container", "horizontal-padding"], [1, "content-container", "week-days"], ["class", "day-col", 4, "ngFor", "ngForOf"], [1, "content-container", "month-days"], [4, "ngFor", "ngForOf"], [1, "day-col"], ["class", "day-col", 3, "click", 4, "ngFor", "ngForOf"], [1, "day-col", 3, "click"], [1, "time-container", "horizontal-padding", "content-container"], [1, "time-col", "hour-col"], [1, "item"], [1, "dp-btn"], [1, "time-col", "minute-col"], ["class", "item", 4, "ngFor", "ngForOf"], ["class", "time-col second-col", 4, "ngIf"], ["class", "time-col meridian-col", 4, "ngIf"], ["themeHover", "", 1, "dp-btn", 3, "click"], [1, "time-col", "second-col"], [1, "time-col", "meridian-col"], ["themeHover", "", 1, "the-item", "dp-btn", 3, "click"]],
    template: function NgPersianDatepickerComponent_Template(rf, ctx) {
      if (rf & 1) {
        const _r1 = ɵɵgetCurrentView();
        ɵɵprojectionDef();
        ɵɵelementStart(0, "div", 13);
        ɵɵprojection(1);
        ɵɵelementStart(2, "div", 14)(3, "div", 15)(4, "div", 16)(5, "div", 17, 0);
        ɵɵlistener("click", function NgPersianDatepickerComponent_Template_div_click_5_listener() {
          ɵɵrestoreView(_r1);
          return ɵɵresetView(ctx.navigate(false));
        });
        ɵɵnamespaceSVG();
        ɵɵelementStart(7, "svg", 18);
        ɵɵelement(8, "path", 19);
        ɵɵelementEnd()();
        ɵɵnamespaceHTML();
        ɵɵelementStart(9, "div", 20, 1);
        ɵɵlistener("click", function NgPersianDatepickerComponent_Template_div_click_9_listener() {
          ɵɵrestoreView(_r1);
          return ɵɵresetView(ctx.nextViewMode());
        });
        ɵɵelementStart(11, "span");
        ɵɵtext(12);
        ɵɵelementEnd()();
        ɵɵelementStart(13, "div", 21, 2);
        ɵɵlistener("click", function NgPersianDatepickerComponent_Template_div_click_13_listener() {
          ɵɵrestoreView(_r1);
          return ɵɵresetView(ctx.navigate(true));
        });
        ɵɵnamespaceSVG();
        ɵɵelementStart(15, "svg", 18);
        ɵɵelement(16, "path", 22);
        ɵɵelementEnd()()();
        ɵɵtemplate(17, NgPersianDatepickerComponent_div_17_Template, 3, 3, "div", 23)(18, NgPersianDatepickerComponent_div_18_Template, 3, 3, "div", 24)(19, NgPersianDatepickerComponent_div_19_Template, 5, 4, "div", 25)(20, NgPersianDatepickerComponent_ng_container_20_Template, 25, 22, "ng-container", 26);
        ɵɵnamespaceHTML();
        ɵɵelementStart(21, "div", 27);
        ɵɵtemplate(22, NgPersianDatepickerComponent_ng_container_22_Template, 5, 5, "ng-container", 26);
        ɵɵelementEnd()()()();
      }
      if (rf & 2) {
        const goBack_r31 = ɵɵreference(6);
        const switchView_r32 = ɵɵreference(10);
        const goForward_r33 = ɵɵreference(14);
        ɵɵadvance(2);
        ɵɵstyleProp("background-color", ctx.uiTheme.background)("color", ctx.uiTheme.text)("border-color", ctx.uiTheme.border);
        ɵɵclassProp("hide", !ctx.uiIsVisible);
        ɵɵproperty("dir", ctx.calendarIsGregorian ? "ltr" : "rtl");
        ɵɵadvance(3);
        ɵɵstyleProp("background-color", goBack_r31.classList.contains("hover") ? ctx.uiTheme.hoverBackground : null);
        ɵɵadvance(2);
        ɵɵstyleProp("fill", goBack_r31.classList.contains("hover") ? ctx.uiTheme.hoverText : ctx.uiTheme.text);
        ɵɵadvance(2);
        ɵɵstyleProp("background-color", switchView_r32.classList.contains("hover") ? ctx.uiTheme.hoverBackground : null)("color", switchView_r32.classList.contains("hover") ? ctx.uiTheme.hoverText : null);
        ɵɵadvance(3);
        ɵɵtextInterpolate(ctx.viewDateTitle);
        ɵɵadvance();
        ɵɵstyleProp("background-color", goForward_r33.classList.contains("hover") ? ctx.uiTheme.hoverBackground : null);
        ɵɵadvance(2);
        ɵɵstyleProp("fill", goForward_r33.classList.contains("hover") ? ctx.uiTheme.hoverText : ctx.uiTheme.text);
        ɵɵadvance(2);
        ɵɵproperty("ngIf", ctx.viewModes[ctx.viewModeIndex] === "year");
        ɵɵadvance();
        ɵɵproperty("ngIf", ctx.viewModes[ctx.viewModeIndex] === "month");
        ɵɵadvance();
        ɵɵproperty("ngIf", ctx.viewModes[ctx.viewModeIndex] === "day");
        ɵɵadvance();
        ɵɵproperty("ngIf", ctx.timeEnable);
        ɵɵadvance(2);
        ɵɵproperty("ngIf", ctx.uiTodayBtnEnable);
      }
    },
    dependencies: [NgForOf, NgIf, ThemeHoverDirective, DecimalPipe, MonthPipe],
    styles: [".datepicker-outer-container[_ngcontent-%COMP%]{vertical-align:top;min-width:200px;font-size:12px;padding-top:8px;padding-bottom:8px;-webkit-user-select:none;user-select:none;text-align:center;border-radius:5px;border-width:1px;border-style:solid}.datepicker-outer-container[dir=rtl][_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .navigation-container[_ngcontent-%COMP%]{flex-direction:row}.datepicker-outer-container[dir=ltr][_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .navigation-container[_ngcontent-%COMP%]{flex-direction:row-reverse}.datepicker-outer-container.hide[_ngcontent-%COMP%]{display:none}.datepicker-outer-container.manual-position[_ngcontent-%COMP%]{width:100%}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .horizontal-padding[_ngcontent-%COMP%]{padding-right:5px;padding-left:5px}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .no-margin-bottom[_ngcontent-%COMP%]{margin-bottom:0!important}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .dp-btn[_ngcontent-%COMP%]{border-radius:5px}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .content-container[_ngcontent-%COMP%]{margin-bottom:8px}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .navigation-container[_ngcontent-%COMP%]{display:flex}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .navigation-container[_ngcontent-%COMP%]   .go-forward[_ngcontent-%COMP%], .datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .navigation-container[_ngcontent-%COMP%]   .switch-view[_ngcontent-%COMP%], .datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .navigation-container[_ngcontent-%COMP%]   .go-back[_ngcontent-%COMP%]{display:inline-block;vertical-align:top;height:24px;cursor:pointer}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .navigation-container[_ngcontent-%COMP%]   .go-forward[_ngcontent-%COMP%], .datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .navigation-container[_ngcontent-%COMP%]   .go-back[_ngcontent-%COMP%]{width:15%;position:relative}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .navigation-container[_ngcontent-%COMP%]   .go-forward[_ngcontent-%COMP%]   svg[_ngcontent-%COMP%], .datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .navigation-container[_ngcontent-%COMP%]   .go-back[_ngcontent-%COMP%]   svg[_ngcontent-%COMP%]{width:12px;height:12px;position:absolute;top:calc(50% - 6px);right:calc(50% - 6px)}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .navigation-container[_ngcontent-%COMP%]   .switch-view[_ngcontent-%COMP%]{width:66%;margin:0 2%;font-weight:700}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .navigation-container[_ngcontent-%COMP%]   .switch-view[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]{vertical-align:sub}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .days-container[_ngcontent-%COMP%]   .day-col[_ngcontent-%COMP%]{display:inline-block;vertical-align:top;width:14.2857142857%;padding:1px}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .days-container[_ngcontent-%COMP%]   .day-col[_ngcontent-%COMP%]   .dp-btn[_ngcontent-%COMP%]{display:flex;flex-direction:column;justify-content:center;align-items:center;min-height:24px}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .days-container[_ngcontent-%COMP%]   .week-days[_ngcontent-%COMP%]   .day-col[_ngcontent-%COMP%]{font-weight:700}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .days-container[_ngcontent-%COMP%]   .month-days[_ngcontent-%COMP%]   .day-col[_ngcontent-%COMP%]{cursor:pointer}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .three-col-per-row[_ngcontent-%COMP%]{display:inline-block;vertical-align:top;width:33.3333333333%;padding:1px;cursor:pointer}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .three-col-per-row[_ngcontent-%COMP%]   .dp-btn[_ngcontent-%COMP%]{display:flex;flex-direction:column;justify-content:center;align-content:center;min-height:36px}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .time-container[_ngcontent-%COMP%]{border-top-width:1px;border-top-style:solid;border-bottom-width:1px;border-bottom-style:solid;height:82px;direction:ltr;display:flex;flex-direction:row;justify-content:center;align-content:center}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .time-container[_ngcontent-%COMP%]   .time-col[_ngcontent-%COMP%]{flex:1;height:100%;overflow:auto}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .time-container[_ngcontent-%COMP%]   .time-col[_ngcontent-%COMP%]:not(.meridian-col)   .item[_ngcontent-%COMP%]:first-of-type{border-bottom-width:1px;border-bottom-style:solid;cursor:auto}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .time-container[_ngcontent-%COMP%]   .time-col[_ngcontent-%COMP%]:not(.meridian-col)   .item[_ngcontent-%COMP%]:last-of-type{border-top-width:1px;border-top-style:solid;cursor:auto}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .time-container[_ngcontent-%COMP%]   .time-col[_ngcontent-%COMP%]   .item[_ngcontent-%COMP%]{padding:1px;cursor:pointer}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .time-container[_ngcontent-%COMP%]   .time-col[_ngcontent-%COMP%]   .item[_ngcontent-%COMP%]:first-of-type{padding-top:3px}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .time-container[_ngcontent-%COMP%]   .time-col[_ngcontent-%COMP%]   .item[_ngcontent-%COMP%]:last-of-type{padding-bottom:3px}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .time-container[_ngcontent-%COMP%]   .time-col[_ngcontent-%COMP%]   .item[_ngcontent-%COMP%]   .dp-btn[_ngcontent-%COMP%]{min-height:24px;display:flex;flex-direction:column;justify-content:center;align-content:center}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .time-container[_ngcontent-%COMP%]   .time-col.meridian-col[_ngcontent-%COMP%]{display:flex;flex-direction:column;justify-content:center}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .the-toolbox[_ngcontent-%COMP%]{display:flex}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .the-toolbox[_ngcontent-%COMP%]   .the-item[_ngcontent-%COMP%]{display:inline-block;vertical-align:top;min-width:40px;cursor:pointer;font-weight:700;padding:3px 5px;margin-left:5px;text-align:center}.datepicker-outer-container[_ngcontent-%COMP%]   .datepicker-inner-container[_ngcontent-%COMP%]   .the-toolbox[_ngcontent-%COMP%]   .the-item[_ngcontent-%COMP%]:last-child{margin-left:0}"]
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NgPersianDatepickerComponent, [{
    type: Component,
    args: [{
      selector: "ng-persian-datepicker",
      standalone: false,
      template: `<div class="datepicker-content">
  <ng-content />
  <div class="datepicker-outer-container"
       [dir]="calendarIsGregorian ? 'ltr' : 'rtl'"
       [style.background-color]="uiTheme.background"
       [style.color]="uiTheme.text"
       [style.border-color]="uiTheme.border"
       [class.hide]="!uiIsVisible">
    <div class="datepicker-inner-container">

      <!-- Start: navigation -->
      <div class="content-container navigation-container horizontal-padding">
        <div class="go-back dp-btn"
             [style.background-color]="goBack.classList.contains('hover') ? uiTheme.hoverBackground : null"
             (click)="navigate(false)" themeHover #goBack>
          <svg xmlns="http://www.w3.org/2000/svg"
               viewBox="0 0 240.823 240.823"
               [style.fill]="goBack.classList.contains('hover') ? uiTheme.hoverText : uiTheme.text">
            <path d="M183.189,111.816L74.892,3.555c-4.752-4.74-12.451-4.74-17.215,0c-4.752,4.74-4.752,12.439,0,17.179
                   l99.707,99.671l-99.695,99.671c-4.752,4.74-4.752,12.439,0,17.191c4.752,4.74,12.463,4.74,17.215,0l108.297-108.261
                   C187.881,124.315,187.881,116.495,183.189,111.816z"></path>
          </svg>
        </div>
        <div class="switch-view dp-btn"
             [style.background-color]="switchView.classList.contains('hover') ? uiTheme.hoverBackground : null"
             [style.color]="switchView.classList.contains('hover') ? uiTheme.hoverText : null"
             (click)="nextViewMode()" themeHover #switchView>
          <span>{{ viewDateTitle }}</span>
        </div>
        <div class="go-forward dp-btn"
             [style.background-color]="goForward.classList.contains('hover') ? uiTheme.hoverBackground : null"
             (click)="navigate(true)" themeHover #goForward>
          <svg xmlns="http://www.w3.org/2000/svg"
               viewBox="0 0 240.823 240.823"
               [style.fill]="goForward.classList.contains('hover') ? uiTheme.hoverText : uiTheme.text">
            <path d="M57.633,129.007L165.93,237.268c4.752,4.74,12.451,4.74,17.215,0c4.752-4.74,4.752-12.439,0-17.179
                   l-99.707-99.671l99.695-99.671c4.752-4.74,4.752-12.439,0-17.191c-4.752-4.74-12.463-4.74-17.215,0L57.621,111.816
                   C52.942,116.507,52.942,124.327,57.633,129.007z"></path>
          </svg>
        </div>
      </div>
      <!-- End: navigation -->

      <!-- Start: year view mode -->
      <div *ngIf="viewModes[viewModeIndex] === 'year'"
           class="years-container horizontal-padding">
        <div class="content-container"
             [class.no-margin-bottom]="!timeEnable && !uiTodayBtnEnable">
          <div class="year-col three-col-per-row"
               (click)="yearClick(year)"
               *ngFor="let year of years">
            <div class="dp-btn"
                 [class.disabled]="year.isYearDisabled"
                 [class.selected]="year.isYearOfSelectedDate"
                 [class.today]="year.isYearOfTodayDate"
                 [style.background-color]="
                   (year.isYearDisabled) ?
                     uiTheme.disabledBackground :
                     (year.isYearOfSelectedDate) ?
                       uiTheme.selectedBackground :
                       (yearCol.classList.contains('hover')) ?
                         uiTheme.hoverBackground :
                         (year.isYearOfTodayDate) ?
                           uiTheme.todayBackground :
                           null
                 "
                 [style.color]="
                   (year.isYearDisabled) ?
                     uiTheme.disabledText :
                     (year.isYearOfSelectedDate) ?
                       uiTheme.selectedText :
                       (yearCol.classList.contains('hover')) ?
                         uiTheme.hoverText :
                         (year.isYearOfTodayDate) ?
                           uiTheme.todayText :
                           null
                 "
                 themeHover #yearCol>
              <span>{{ year.value }}</span>
            </div>
          </div>
        </div>
      </div>
      <!-- End: year view mode -->

      <!-- Start: month view mode -->
      <div *ngIf="viewModes[viewModeIndex] === 'month'"
           class="months-container horizontal-padding">
        <div class="content-container"
             [class.no-margin-bottom]="!timeEnable && !uiTodayBtnEnable">
          <div class="month-col three-col-per-row"
               (click)="monthClick(month)"
               *ngFor="let month of months">
            <div class="dp-btn"
                 [class.disabled]="month.isMonthDisabled"
                 [class.selected]="month.isMonthOfSelectedDate"
                 [class.today]="month.isMonthOfTodayDate"
                 [style.background-color]="
                   (month.isMonthDisabled) ?
                     uiTheme.disabledBackground :
                     (month.isMonthOfSelectedDate) ?
                       uiTheme.selectedBackground :
                       (monthCol.classList.contains('hover')) ?
                         uiTheme.hoverBackground :
                         (month.isMonthOfTodayDate) ?
                           uiTheme.todayBackground :
                           null
                 "
                 [style.color]="
                   (month.isMonthDisabled) ?
                     uiTheme.disabledText :
                     (month.isMonthOfSelectedDate) ?
                       uiTheme.selectedText :
                       (monthCol.classList.contains('hover')) ?
                         uiTheme.hoverText :
                         (month.isMonthOfTodayDate) ?
                           uiTheme.todayText :
                           null
                 "
                 themeHover #monthCol>
              <span>{{ month.indexValue | month: calendarIsGregorian }}</span>
            </div>
          </div>
        </div>
      </div>
      <!-- End: month view mode -->

      <!-- Start: day view mode -->
      <div *ngIf="viewModes[viewModeIndex] === 'day'"
           class="days-container horizontal-padding">
        <div class="content-container week-days">
          <div class="day-col"
               *ngFor="let weekDay of weekDays">
            <span>{{ weekDay }}</span>
          </div>
        </div>
        <div class="content-container month-days"
             [class.no-margin-bottom]="!timeEnable && !uiTodayBtnEnable">
          <ng-container *ngFor="let row of days">
            <div class="day-col"
                 (click)="dayClick(day)"
                 *ngFor="let day of row">
              <div class="dp-btn"
                   [class.disabled]="day.isDayDisabled"
                   [class.selected]="day.isDayOfSelectedDate"
                   [class.today]="day.isDayOfTodayDate"
                   [class.other-month]="!day.isDayInCurrentMonth"
                   [style.background-color]="
                     (day.isDayDisabled) ?
                       uiTheme.disabledBackground :
                       (day.isDayOfSelectedDate) ?
                         uiTheme.selectedBackground :
                         (dayCol.classList.contains('hover')) ?
                           uiTheme.hoverBackground :
                           (day.isDayOfTodayDate) ?
                             uiTheme.todayBackground :
                             (!day.isDayInCurrentMonth) ?
                               uiTheme.otherMonthBackground :
                               null
                   "
                   [style.color]="
                     (day.isDayDisabled) ?
                       uiTheme.disabledText :
                       (day.isDayOfSelectedDate) ?
                         uiTheme.selectedText :
                         (dayCol.classList.contains('hover')) ?
                           uiTheme.hoverText :
                           (day.isDayOfTodayDate) ?
                             uiTheme.todayText :
                             (!day.isDayInCurrentMonth) ?
                               uiTheme.otherMonthText :
                               null
                   "
                   themeHover #dayCol>
                <span>{{ day.value }}</span>
              </div>
            </div>
          </ng-container>
        </div>
      </div>
      <!-- End: day view mode -->

      <!-- Start: time picker -->
      <ng-container *ngIf="timeEnable">
        <div class="time-container horizontal-padding content-container"
             [style.border-top-color]="uiTheme.timeBorder"
             [style.border-bottom-color]="uiTheme.timeBorder">
          <div class="time-col hour-col">
            <div class="item"
                 [style.border-bottom-color]="uiTheme.border">
              <div class="dp-btn">
                <span>{{ hour }}</span>
              </div>
            </div>
            <ng-container *ngIf="!timeMeridian">
              <div class="item"
                   *ngFor="let _ of [].constructor(24); let i = index">
                <div class="dp-btn"
                     (click)="setHour(i)"
                     [class.selected]="hour === i"
                     [style.background-color]="
                       (hour === i) ?
                         uiTheme.selectedBackground :
                         (hourCol.classList.contains('hover')) ?
                           uiTheme.hoverBackground :
                           null
                     "
                     [style.color]="
                       (hour === i) ?
                         uiTheme.selectedText :
                         (hourCol.classList.contains('hover')) ?
                           uiTheme.hoverText :
                           null
                     " themeHover #hourCol>
                  <span>{{ i | number: '2.0' }}</span>
                </div>
              </div>
            </ng-container>
            <ng-container *ngIf="timeMeridian">
              <div class="item"
                   *ngFor="let _ of [].constructor(12); let i = index">
                <div class="dp-btn"
                     (click)="set12Hour(i + 1)"
                     [class.selected]="
                       (hour === 0 && (i + 1) === 12) ||
                       (hour >= 1 && hour <= 12 && hour === (i + 1)) ||
                       (hour > 12 && hour === ((i + 1) + 12))
                     "
                     [style.background-color]="
                       (
                         (hour === 0 && (i + 1) === 12) ||
                         (hour >= 1 && hour <= 12 && hour === (i + 1)) ||
                         (hour > 12 && hour === ((i + 1) + 12))
                       ) ?
                         uiTheme.selectedBackground :
                         (hour12Col.classList.contains('hover')) ?
                           uiTheme.hoverBackground :
                           null
                     "
                     [style.color]="
                       (
                         (hour === 0 && (i + 1) === 12) ||
                         (hour >= 1 && hour <= 12 && hour === (i + 1)) ||
                         (hour > 12 && hour === ((i + 1) + 12))
                       ) ?
                         uiTheme.selectedText :
                         (hour12Col.classList.contains('hover')) ?
                           uiTheme.hoverText :
                           null
                     " themeHover #hour12Col>
                  <span>{{ (i + 1) | number: '2.0' }}</span>
                </div>
              </div>
            </ng-container>
            <div class="item"
                 [style.border-top-color]="uiTheme.border">
              <div class="dp-btn">
                <span>{{ hour }}</span>
              </div>
            </div>
          </div>
          <div class="time-col minute-col">
            <div class="item"
                 [style.border-bottom-color]="uiTheme.border">
              <div class="dp-btn">
                <span>{{ minute }}</span>
              </div>
            </div>
            <div class="item"
                 *ngFor="let _ of [].constructor(60); let i = index">
              <div class="dp-btn"
                   (click)="setMinute(i)"
                   [class.selected]="minute === i"
                   [style.background-color]="
                     (minute === i) ?
                       uiTheme.selectedBackground :
                       (minuteCol.classList.contains('hover')) ?
                         uiTheme.hoverBackground :
                         null
                   "
                   [style.color]="
                     (minute === i) ?
                       uiTheme.selectedText :
                       (minuteCol.classList.contains('hover')) ?
                         uiTheme.hoverText :
                         null
                   " themeHover #minuteCol>
                <span>{{ i | number: '2.0' }}</span>
              </div>
            </div>
            <div class="item"
                 [style.border-top-color]="uiTheme.border">
              <div class="dp-btn">
                <span>{{ minute }}</span>
              </div>
            </div>
          </div>
          <div *ngIf="timeShowSecond"
               class="time-col second-col">
            <div class="item"
                 [style.border-bottom-color]="uiTheme.border">
              <div class="dp-btn">
                <span>{{ second }}</span>
              </div>
            </div>
            <div class="item"
                 *ngFor="let _ of [].constructor(60); let i = index">
              <div class="dp-btn"
                   (click)="setSecond(i)"
                   [class.selected]="second === i"
                   [style.background-color]="
                     (second === i) ?
                       uiTheme.selectedBackground :
                       (secondCol.classList.contains('hover')) ?
                         uiTheme.hoverBackground :
                         null
                   "
                   [style.color]="
                     (second === i) ?
                       uiTheme.selectedText :
                       (secondCol.classList.contains('hover')) ?
                         uiTheme.hoverText :
                         null
                   " themeHover #secondCol>
                <span>{{ i | number: '2.0' }}</span>
              </div>
            </div>
            <div class="item"
                 [style.border-top-color]="uiTheme.border">
              <div class="dp-btn">
                <span>{{ second }}</span>
              </div>
            </div>
          </div>
          <div *ngIf="timeMeridian"
               class="time-col meridian-col">
            <div class="item">
              <div class="dp-btn"
                   (click)="toggleAmPm('AM')"
                   [class.selected]="hour < 12"
                   [style.background-color]="
                     (hour < 12) ?
                       uiTheme.selectedBackground :
                       (amCol.classList.contains('hover')) ?
                         uiTheme.hoverBackground :
                         null
                   "
                   [style.color]="
                     (hour < 12) ?
                       uiTheme.selectedText :
                       (amCol.classList.contains('hover')) ?
                         uiTheme.hoverText :
                         null
                   " themeHover #amCol>
                <span>AM</span>
              </div>
            </div>
            <div class="item">
              <div class="dp-btn"
                   (click)="toggleAmPm('PM')"
                   [class.selected]="hour >= 12"
                   [style.background-color]="
                     (hour >= 12) ?
                       uiTheme.selectedBackground :
                       (pmCol.classList.contains('hover')) ?
                         uiTheme.hoverBackground :
                         null
                   "
                   [style.color]="
                     (hour >= 12) ?
                       uiTheme.selectedText :
                       (pmCol.classList.contains('hover')) ?
                         uiTheme.hoverText :
                         null
                   " themeHover #pmCol>
                <span>PM</span>
              </div>
            </div>
          </div>
        </div>
      </ng-container>
      <!-- End: time picker -->

      <!-- Start: toolbox -->
      <div class="the-toolbox horizontal-padding">
        <ng-container *ngIf="uiTodayBtnEnable">
          <div class="the-item dp-btn"
               [style.background-color]="today.classList.contains('hover') ? uiTheme.hoverBackground : null"
               [style.color]="today.classList.contains('hover') ? uiTheme.hoverText : null"
               (click)="selectToday()" themeHover #today>
            <span>{{ calendarIsGregorian ? 'Today' : 'امروز' }}</span>
          </div>
        </ng-container>
      </div>
      <!-- End: toolbox -->

    </div>
  </div>
</div>
`,
      styles: [".datepicker-outer-container{vertical-align:top;min-width:200px;font-size:12px;padding-top:8px;padding-bottom:8px;-webkit-user-select:none;user-select:none;text-align:center;border-radius:5px;border-width:1px;border-style:solid}.datepicker-outer-container[dir=rtl] .datepicker-inner-container .navigation-container{flex-direction:row}.datepicker-outer-container[dir=ltr] .datepicker-inner-container .navigation-container{flex-direction:row-reverse}.datepicker-outer-container.hide{display:none}.datepicker-outer-container.manual-position{width:100%}.datepicker-outer-container .datepicker-inner-container .horizontal-padding{padding-right:5px;padding-left:5px}.datepicker-outer-container .datepicker-inner-container .no-margin-bottom{margin-bottom:0!important}.datepicker-outer-container .datepicker-inner-container .dp-btn{border-radius:5px}.datepicker-outer-container .datepicker-inner-container .content-container{margin-bottom:8px}.datepicker-outer-container .datepicker-inner-container .navigation-container{display:flex}.datepicker-outer-container .datepicker-inner-container .navigation-container .go-forward,.datepicker-outer-container .datepicker-inner-container .navigation-container .switch-view,.datepicker-outer-container .datepicker-inner-container .navigation-container .go-back{display:inline-block;vertical-align:top;height:24px;cursor:pointer}.datepicker-outer-container .datepicker-inner-container .navigation-container .go-forward,.datepicker-outer-container .datepicker-inner-container .navigation-container .go-back{width:15%;position:relative}.datepicker-outer-container .datepicker-inner-container .navigation-container .go-forward svg,.datepicker-outer-container .datepicker-inner-container .navigation-container .go-back svg{width:12px;height:12px;position:absolute;top:calc(50% - 6px);right:calc(50% - 6px)}.datepicker-outer-container .datepicker-inner-container .navigation-container .switch-view{width:66%;margin:0 2%;font-weight:700}.datepicker-outer-container .datepicker-inner-container .navigation-container .switch-view span{vertical-align:sub}.datepicker-outer-container .datepicker-inner-container .days-container .day-col{display:inline-block;vertical-align:top;width:14.2857142857%;padding:1px}.datepicker-outer-container .datepicker-inner-container .days-container .day-col .dp-btn{display:flex;flex-direction:column;justify-content:center;align-items:center;min-height:24px}.datepicker-outer-container .datepicker-inner-container .days-container .week-days .day-col{font-weight:700}.datepicker-outer-container .datepicker-inner-container .days-container .month-days .day-col{cursor:pointer}.datepicker-outer-container .datepicker-inner-container .three-col-per-row{display:inline-block;vertical-align:top;width:33.3333333333%;padding:1px;cursor:pointer}.datepicker-outer-container .datepicker-inner-container .three-col-per-row .dp-btn{display:flex;flex-direction:column;justify-content:center;align-content:center;min-height:36px}.datepicker-outer-container .datepicker-inner-container .time-container{border-top-width:1px;border-top-style:solid;border-bottom-width:1px;border-bottom-style:solid;height:82px;direction:ltr;display:flex;flex-direction:row;justify-content:center;align-content:center}.datepicker-outer-container .datepicker-inner-container .time-container .time-col{flex:1;height:100%;overflow:auto}.datepicker-outer-container .datepicker-inner-container .time-container .time-col:not(.meridian-col) .item:first-of-type{border-bottom-width:1px;border-bottom-style:solid;cursor:auto}.datepicker-outer-container .datepicker-inner-container .time-container .time-col:not(.meridian-col) .item:last-of-type{border-top-width:1px;border-top-style:solid;cursor:auto}.datepicker-outer-container .datepicker-inner-container .time-container .time-col .item{padding:1px;cursor:pointer}.datepicker-outer-container .datepicker-inner-container .time-container .time-col .item:first-of-type{padding-top:3px}.datepicker-outer-container .datepicker-inner-container .time-container .time-col .item:last-of-type{padding-bottom:3px}.datepicker-outer-container .datepicker-inner-container .time-container .time-col .item .dp-btn{min-height:24px;display:flex;flex-direction:column;justify-content:center;align-content:center}.datepicker-outer-container .datepicker-inner-container .time-container .time-col.meridian-col{display:flex;flex-direction:column;justify-content:center}.datepicker-outer-container .datepicker-inner-container .the-toolbox{display:flex}.datepicker-outer-container .datepicker-inner-container .the-toolbox .the-item{display:inline-block;vertical-align:top;min-width:40px;cursor:pointer;font-weight:700;padding:3px 5px;margin-left:5px;text-align:center}.datepicker-outer-container .datepicker-inner-container .the-toolbox .the-item:last-child{margin-left:0}\n"]
    }]
  }], () => [{
    type: ElementRef
  }], {
    _formControlDirective: [{
      type: ContentChild,
      args: [FormControlDirective, {
        static: false
      }]
    }],
    _formControlName: [{
      type: ContentChild,
      args: [FormControlName, {
        static: false
      }]
    }],
    _calendarIsGregorian: [{
      type: Input,
      args: ["calendarIsGregorian"]
    }],
    inputDateValue: [{
      type: Input,
      args: ["dateValue"]
    }],
    dateInitValue: [{
      type: Input
    }],
    dateIsGregorian: [{
      type: Input
    }],
    _dateFormat: [{
      type: Input,
      args: ["dateFormat"]
    }],
    dateGregorianFormat: [{
      type: Input
    }],
    _dateMin: [{
      type: Input,
      args: ["dateMin"]
    }],
    _dateMax: [{
      type: Input,
      args: ["dateMax"]
    }],
    _timeEnable: [{
      type: Input,
      args: ["timeEnable"]
    }],
    _timeShowSecond: [{
      type: Input,
      args: ["timeShowSecond"]
    }],
    _timeMeridian: [{
      type: Input,
      args: ["timeMeridian"]
    }],
    _uiTheme: [{
      type: Input,
      args: ["uiTheme"]
    }],
    uiIsVisible: [{
      type: Input
    }],
    uiHideOnOutsideClick: [{
      type: Input
    }],
    uiHideAfterSelectDate: [{
      type: Input
    }],
    _uiYearView: [{
      type: Input,
      args: ["uiYearView"]
    }],
    _uiMonthView: [{
      type: Input,
      args: ["uiMonthView"]
    }],
    uiInitViewMode: [{
      type: Input
    }],
    uiTodayBtnEnable: [{
      type: Input
    }],
    dateOnInit: [{
      type: Output
    }],
    dateOnSelect: [{
      type: Output
    }],
    uiIsVisibleChange: [{
      type: Output
    }],
    onInsideClick: [{
      type: HostListener,
      args: ["click"]
    }],
    onOutsideClick: [{
      type: HostListener,
      args: ["document:click"]
    }]
  });
})();
var NgPersianDatepickerModule = class _NgPersianDatepickerModule {
  static ɵfac = function NgPersianDatepickerModule_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NgPersianDatepickerModule)();
  };
  static ɵmod = ɵɵdefineNgModule({
    type: _NgPersianDatepickerModule,
    declarations: [NgPersianDatepickerComponent, ThemeHoverDirective, MonthPipe],
    imports: [CommonModule],
    exports: [NgPersianDatepickerComponent]
  });
  static ɵinj = ɵɵdefineInjector({
    imports: [CommonModule]
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NgPersianDatepickerModule, [{
    type: NgModule,
    args: [{
      declarations: [NgPersianDatepickerComponent, ThemeHoverDirective, MonthPipe],
      imports: [CommonModule],
      exports: [NgPersianDatepickerComponent]
    }]
  }], null, null);
})();
export {
  NgPersianDatepickerComponent,
  NgPersianDatepickerModule,
  defaultTheme
};
//# sourceMappingURL=ng-persian-datepicker.js.map
