
import { Component, OnInit } from '@angular/core';
import { ApiService, NotificationService, BaseComponent, Globals } from 'app/my-app/_shared';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent extends BaseComponent implements OnInit {

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

    this.form = this._formBuilder.group({
        OldPassword: ['', Validators.required],
        NewPassword: ['', Validators.required],
    });

  }

  save(): void {
    this.Globals.IsBusy = true;
    this.apiService.changePassword(this.form.value).subscribe((data) => {
        this.apiService.setMessage2(data);
        this.Globals.IsBusy = false;
    });
}

}
