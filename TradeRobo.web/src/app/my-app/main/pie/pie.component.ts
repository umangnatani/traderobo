import { Component, OnInit } from '@angular/core';
import { ApiService, NotificationService, BaseComponent, Globals } from 'app/my-app/_shared';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';



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

  }

}
