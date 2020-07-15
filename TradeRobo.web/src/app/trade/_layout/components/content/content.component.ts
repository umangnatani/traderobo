import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { NotificationService, ApiService } from 'app/trade/_services';
import { Globals } from 'app/trade/_helpers/globals';

@Component({
    selector     : 'content',
    templateUrl  : './content.component.html',
    styleUrls    : ['./content.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class ContentComponent implements OnInit
{

    Title;
    Error;

    globals: Globals;
    /**
     * Constructor
     */
    constructor(globals: Globals, 
                public apiService: ApiService,
                private notificationService: NotificationService)
    {
        this.globals = globals;
        // this.apiService.Title
    }

    ngOnInit(): void
    {
        this.Title = this.globals.PageTitle;
        console.log(this.Title);
        this.notificationService.currentError.subscribe(val => this.Error = val);
      
    }
}
