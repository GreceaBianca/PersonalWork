import { OnDestroy } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
export abstract class SelfUnsubscriberBase implements OnDestroy {
  protected ngUnsubscribe: Subject<any> = new Subject();
  protected subscriptions = new Array<Subscription>();
  protected onDestroy: () => unknown;
  ngOnDestroy(): void {
    if (this.onDestroy) {
      this.onDestroy();
    }
    if (!!this.subscriptions && this.subscriptions.length > 0) {
      for (let sub of this.subscriptions) {
        sub.unsubscribe();
      }
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
