import { Component, OnInit } from '@angular/core';
import { NotificationService, ApiService, Globals, BaseComponent } from 'app/my-app/_shared';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-user-pref',
  templateUrl: './user-pref.component.html',
  styleUrls: ['./user-pref.component.scss']
})
export class UserPrefComponent extends BaseComponent implements OnInit {

    form: FormGroup;

    constructor(
        private _formBuilder: FormBuilder,
        protected apiService: ApiService,
        private notificationService: NotificationService,
        private globals: Globals
    ) {
        super(apiService);

    }

  ngOnInit(): void {
  }

  refreshMA(){
    this.Globals.IsBusy = true;
      this.apiService.refreshMA().subscribe((data) => {
        this.apiService.setMessage2(data);
        this.Globals.IsBusy = false;
    });
  }

  toggleProxy(){
    this.Globals.IsBusy = true;
      this.apiService.toggleProxy().subscribe((data) => {
        this.apiService.setMessage2(data);
        this.Globals.IsBusy = false;
    });
  }

}
