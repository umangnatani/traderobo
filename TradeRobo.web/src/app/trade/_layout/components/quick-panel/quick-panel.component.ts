import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { NotificationService, ApiService, AuthenticationService } from 'app/trade/_services';

@Component({
    selector     : 'quick-panel',
    templateUrl  : './quick-panel.component.html',
    styleUrls    : ['./quick-panel.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class QuickPanelComponent implements OnInit, OnDestroy
{
    tickers: ApiModel.FavStocks[];

    // Private
    private _unsubscribeAll: Subject<any>;

    constructor(
        private notificationService: NotificationService,
        private apiService: ApiService,
        private service: AuthenticationService
    )
    {
        // Set the defaults
     
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {

        const currentUser = this.service.currentUserValue;
        if (currentUser) {
            this.apiService.getFavStocks().subscribe((data) => {
                this.tickers = data;
            });
            // Subscribe to the events
        }


      
      
    }

    selectTicker(val: string){
        console.log (val);
        this.notificationService.changSymbol(val);
    }
    /**
     * On destroy
     */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}
