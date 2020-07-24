import { NotificationService } from 'app/trade/_services/notification.service';
import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'app/trade/_shared';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService } from 'app/trade/_services/api.service';
import { Globals } from 'app/trade/_helpers';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent extends BaseComponent implements OnInit {

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

    this.apiService.setTile('Maintain Users');
    // this.globals.PageTitle = 'Place a TD Trade';

    this.form = this._formBuilder.group({
        UserName: ['', Validators.required],
        Password: ['', Validators.required],
    });

  }

  save(): void {
    this.Globals.IsBusy = true;
    this.apiService.saveUser(this.form.value).subscribe((data) => {
        this.apiService.setMessage(data);
        this.Globals.IsBusy = false;
    });
}

}
