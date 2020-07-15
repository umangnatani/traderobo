import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import {MatTabsModule} from '@angular/material/tabs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import {TableModule} from 'primeng/table';

import { FuseSharedModule } from '@fuse/shared.module';

import { RobinhoodComponent } from './main';
import { LoginComponent } from './main';
import { AuthComponent } from './main';
import { OrderComponent } from './main';
import { TDOrderComponent } from './main';
import { FolioComponent } from './main';
import { AuthGuard  } from 'app/trade/_helpers';
import { AppHeaderComponent } from './_shared/component';

const routes: Routes = [
    {
        path     : 'robinhood',
        component: RobinhoodComponent,
        canActivate: [AuthGuard]
    },
    {
        path     : 'td',
        component: TDOrderComponent,
        canActivate: [AuthGuard]
    },

    {
        path     : 'login',
        component: LoginComponent
    }
];

@NgModule({
    declarations: [
        RobinhoodComponent,
        LoginComponent,
        OrderComponent,
        FolioComponent,
        TDOrderComponent,
        AuthComponent,
        AppHeaderComponent
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
        MatCheckboxModule,
        NgxDatatableModule,
        TableModule
    ],
    exports: [OrderComponent]
})
export class TradeModule
{
}
