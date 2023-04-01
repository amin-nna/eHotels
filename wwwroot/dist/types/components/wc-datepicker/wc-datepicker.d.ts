import { EventEmitter } from '../../stencil-public-runtime';
export declare type WCDatepickerLabels = {
  clearButton: string;
  monthSelect: string;
  nextMonthButton: string;
  nextYearButton: string;
  picker: string;
  previousMonthButton: string;
  previousYearButton: string;
  todayButton: string;
  yearSelect: string;
};
export interface MonthChangedEventDetails {
  month: number;
  year: number;
}
export declare class WCDatepicker {
  el: HTMLElement;
  clearButtonContent?: string;
  disabled?: boolean;
  disableDate?: (date: Date) => boolean;
  elementClassName?: string;
  firstDayOfWeek?: number;
  range?: boolean;
  labels?: WCDatepickerLabels;
  locale?: string;
  nextMonthButtonContent?: string;
  nextYearButtonContent?: string;
  previousMonthButtonContent?: string;
  previousYearButtonContent?: string;
  showClearButton?: boolean;
  showMonthStepper?: boolean;
  showTodayButton?: boolean;
  showYearStepper?: boolean;
  startDate?: string;
  todayButtonContent?: string;
  value?: Date | Date[];
  currentDate: Date;
  hoveredDate: Date;
  weekdays: string[][];
  selectDate: EventEmitter<string | string[] | undefined>;
  changeMonth: EventEmitter<MonthChangedEventDetails>;
  private moveFocusAfterMonthChanged;
  componentWillLoad(): void;
  watchFirstDayOfWeek(): void;
  watchLocale(): void;
  watchRange(): void;
  watchStartDate(): void;
  watchValue(): void;
  componentDidRender(): void;
  private init;
  private updateWeekdays;
  private getClassName;
  private getCalendarRows;
  private getTitle;
  private focusDate;
  private updateCurrentDate;
  private onSelectDate;
  private isRangeValue;
  private nextMonth;
  private nextYear;
  private previousMonth;
  private previousYear;
  private showToday;
  private clear;
  private onClick;
  private onMonthSelect;
  private onYearSelect;
  private onKeyDown;
  private onMouseEnter;
  private onMouseLeave;
  render(): any;
}