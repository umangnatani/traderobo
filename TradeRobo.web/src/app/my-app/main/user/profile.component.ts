import { Component, OnInit } from '@angular/core';
import { ApiService, NotificationService, BaseComponent, Globals } from 'app/my-app/_shared';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent extends BaseComponent implements OnInit {

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
        Name: ['', Validators.required],
        Email: [''],
    });

  }

  save(): void {
    this.Globals.IsBusy = true;
    this.apiService.saveProfile(this.form.value).subscribe((data) => {
        this.apiService.setMessage2(data);
        this.Globals.IsBusy = false;
    });
}

}
