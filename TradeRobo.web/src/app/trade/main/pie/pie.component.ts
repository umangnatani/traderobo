import { NotificationService } from 'app/trade/_services/notification.service';
import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'app/trade/_shared';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService } from 'app/trade/_services/api.service';
import { Globals } from 'app/trade/_helpers';


@Component({
  selector: 'app-pie',
  templateUrl: './pie.component.html',
  styleUrls: ['./pie.component.scss']
})
export class PieComponent extends BaseComponent implements OnInit {

    constructor(
        private _formBuilder: FormBuilder,
        protected apiService: ApiService,
        private notificationService: NotificationService,
        private globals: Globals
    ) {
        super(apiService);

    }

  ngOnInit(): void {
    this.apiService.setTile('Manage your Pies');

  }

}
