import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import {MatTabsModule} from '@angular/material/tabs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { FuseSharedModule } from '@fuse/shared.module';

import { RobinhoodComponent } from './robinhood/robinhood.component';
import { LoginComponent } from './robinhood/login.component';
import { OrderComponent } from './robinhood/order.component';
import { FolioComponent } from './robinhood/folio.component';

const routes: Routes = [
    {
        path     : 'robinhood',
        component: RobinhoodComponent
    }
];

@NgModule({
    declarations: [
        RobinhoodComponent,
        LoginComponent,
        OrderComponent,
        FolioComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatSelectModule,
        MatStepperModule,
        MatTableModule,
        MatProgressSpinnerModule,
        MatCardModule,
        MatTabsModule,
        FuseSharedModule,
    ],
    exports: [OrderComponent]
})
export class RobinhoodModule
{
}
