import Vue from 'vue'
import { ElementUIComponent } from 'element-ui/types/component';

/** ElementUI component common definition */
// declare class ElementUIComponent extends Vue {
//   /** Install component into Vue */
//   static install (vue: typeof Vue): void
// }

declare class ElScrollbar extends ElementUIComponent {
  native: boolean
  wrapStyle: any
  wrapClass: any
  viewClass: any
  viewStyle: any
  noresize: boolean
  tag: string
}

declare module 'element-ui/types' {
  interface Scrollbar extends ElScrollbar {}
  export class Scrollbar extends ElScrollbar {}
}