import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'app/trade/_shared';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from 'app/trade/_services/api.service';

@Component({
    selector: 'app-folio',
    templateUrl: './folio.component.html',
    styleUrls: ['./folio.component.scss']
})
export class FolioComponent extends BaseComponent implements OnInit {

    pieValue = 'fav';
    folioForm: FormGroup;

    dataSource;

    PieDetails: ApiModel.PieDetail[];

    Total = 0;

    Pies;
    // columns = [
    //     { prop: 'Symbol', summaryFunc: () => null },
    //     { prop: 'Weight', summaryFunc: cells => this.summaryWeight(cells) }
    //   ];

    // columns = [{ prop: 'Symbol' }, { prop: 'Weight' }];


    constructor(
        private _formBuilder: FormBuilder,
        protected apiService: ApiService
    ) {
        super(apiService);

    }


    ngOnInit(): void {
        this.folioForm = this._formBuilder.group({
            PieId: [],
            Amount: [100, Validators.required],
        });


        // console.log(this.token.isAuthenticated);

        this.apiService.getFolio().subscribe((data) => {
            this.Pies = data;
        });

    }

    public savePie(){
        // console.log(this.PieDetails);
        this.apiService.savePieDetail(this.PieDetails).subscribe((data) => {
            console.log(data);
            this.apiService.setMessage(data);
        });
    }

    public addNew() {
        const pie: ApiModel.PieDetail = {Id: 0, Symbol: '', Weight: 0, PieId: this.folioForm.controls.PieId.value, Enabled: true };
        this.PieDetails.push(pie);
      }

    public calcTotal(val: number) {
        this.Total = this.getTotal();
      }

    private getTotal() {
        return this.PieDetails.reduce((a, b) => Number(a) + Number((b['Weight'] || 0)), 0);
      }

    selectFolio(pie): void {
        this.folioForm.controls.PieId.setValue(pie.Id);
        this.pieValue = pie.Name;

        this.apiService.getPieDetail(pie.Id).subscribe((data) => {
            this.PieDetails = data;
            this.Total = this.getTotal();
        });
    }

    // public calculateTotal() {
    //     return this.players.reduce((accum, curr) => accum + curr.goals, 0);
    //   }






    placeFolioOrder(): void {
        this.Globals.IsBusy = true;
        this.apiService.placeFolioOrder(this.folioForm.value).subscribe((data) => {
            // console.log(data);
            this.dataSource = data;
            this.Globals.IsBusy = false;
        });
    }


}
