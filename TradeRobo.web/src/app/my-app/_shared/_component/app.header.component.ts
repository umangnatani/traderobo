import { Component, ViewEncapsulation, OnInit, Input } from '@angular/core';
import { ApiService, NotificationService, BaseComponent, Globals } from 'app/my-app/_shared';

@Component({
  selector: 'app-header',
  templateUrl: './app.header.component.html',
  styleUrls: ['./app.header.component.scss']
})
export class AppHeaderComponent implements OnInit {

    @Input() Title: string;
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
        // this.Title = this.globals.PageTitle;
        // console.log(this.Title);
        // this.notificationService.currentError.subscribe(val => this.Error = val);
      
    }

}





