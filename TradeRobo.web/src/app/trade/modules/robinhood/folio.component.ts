import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'app/trade/shared/component/base.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from 'app/trade/shared/services/api.service';

@Component({
  selector: 'app-folio',
  templateUrl: './folio.component.html',
  styleUrls: ['./folio.component.scss']
})
export class FolioComponent extends BaseComponent implements OnInit {

    folioValue: string;
    folioForm: FormGroup;
  
    dataSource;

    

    folios;

    constructor(
        private _formBuilder: FormBuilder,
        protected apiService: ApiService
    ) {
        super(apiService);

    }


  ngOnInit(): void {
    this.folioForm = this._formBuilder.group({
        Folio: ['fav.csv', Validators.required],
        Amount: [100, Validators.required],
    });

    
    // console.log(this.token.isAuthenticated);

    this.apiService.getFolio().subscribe((data) => {
        this.folios = data;
    });

  }

  selectFolio(val: string): void{
    this.folioForm.controls.Folio.setValue(val);
    this.folioValue = val;
}






placeFolioOrder(): void {
    this.Globals.IsBusy = true;
    this.apiService.placeFolioOrder(this.folioForm.value).subscribe((data) => {
        // console.log(data);
        this.dataSource = data;
        this.Globals.IsBusy = false;
    });
}


}
