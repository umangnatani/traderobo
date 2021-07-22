import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ApiService, NotificationService, BaseComponent, Globals, AuthenticationService } from 'app/my-app/_shared';

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

        // this.service.currentUser.subscribe(val => {
        //     if (val){
        //         this.apiService.getFavStocks().subscribe((data) => {
        //             this.tickers = data;
        //         });
        //     }
        // });

      
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
