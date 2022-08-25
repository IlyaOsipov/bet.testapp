import { NgModule } from '@angular/core';
import { LetDirective } from './ng-let.directive';

@NgModule({
  declarations: [LetDirective],
  exports: [LetDirective]
})
export class NgLetDirectiveModule {}
