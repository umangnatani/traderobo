import { NotificationService } from 'app/trade/_services/notification.service';
import { Component, OnInit, Input } from '@angular/core';
import { BaseComponent } from 'app/trade/_shared';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService } from 'app/trade/_services/api.service';
import { Globals } from 'app/trade/_helpers';

@Component({
  selector: 'app-pie-detail',
  templateUrl: './pie-detail.component.html',
  styleUrls: ['./pie-detail.component.scss']
})
export class PieDetailComponent extends BaseComponent implements OnInit {

    activePie: ApiModel.Pie;
    Total;
    Pies: ApiModel.Pie[];

    form: FormGroup;
    
    PieDetails: ApiModel.PieDetail[];

    isEdit = false;

    columnDefs = [
        {field: 'Symbol' },
        {field: 'Weight' }
    ];

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
        Id: [0],
        Name: ['', Validators.required],
        Desc: [],
        UserId: [0],
    });

    this.loadPies();

  }

  loadPies(){
    this.apiService.getPies().subscribe((data) => {
        this.Pies = data;
        // console.log(this.Pies);
    });
  }

  get f() { return this.form.controls; }

  selectFolio(pie): void {
    this.activePie = pie;
    this.isEdit = true;
    // console.log(this.activePie);
    this.form.patchValue(pie);
    this.apiService.getPieDetail(pie.Id).subscribe((data) => {
        this.PieDetails = data;
        this.Total = this.getTotal();
    });
}

public savePie(){
    // console.log(this.PieDetails);
    this.apiService.savePie(this.form.value).subscribe((data) => {
        // console.log(data);
        this.apiService.setMessage2(data);
        this.loadPies();
        // this.activePie = this.form.value;
    });
}

  public savePieDetail(){
    // console.log(this.PieDetails);
    this.apiService.savePieDetail(this.PieDetails).subscribe((data) => {
        console.log(data);
        this.apiService.setMessage(data);
    });
}

public addNewPie() {
    const pie = {Id: 0, Desc: '', UserId: 0, Name: ''};
    this.form.patchValue(pie);
    this.PieDetails = null;
    this.isEdit = true;
  }

public addNew() {
    const pieDetail: ApiModel.PieDetail = {Id: 0, Symbol: '', Weight: 0, PieId: this.activePie.Id, Enabled: true };
    this.PieDetails.push(pieDetail);
  }

public calcTotal(val: number) {
    this.Total = this.getTotal();
  }

private getTotal() {
    return this.PieDetails.reduce((a, b) => Number(a) + Number((b['Weight'] || 0)), 0);
  }


}
