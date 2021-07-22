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
import { AgGridModule } from 'ag-grid-angular';

import { FuseSharedModule } from '@fuse/shared.module';

import { RobinhoodComponent } from './main';
import { LoginComponent } from './main';
import { AuthComponent } from './main';
import { OrderComponent } from './main';
import { TDOrderComponent } from './main';
import { FolioComponent } from './main';
import { AuthGuard  } from 'app/my-app/_shared';
import { AppHeaderComponent } from './_shared';
import { UserComponent } from './main';
import { PieComponent } from './main';
import { PieDetailComponent } from './main/pie/pie-detail.component';
import { PositionComponent } from './main/robinhood/position.component';
import { BuybackComponent } from './main/robinhood/buyback.component';
import { ChangePasswordComponent } from './main/user/change-password.component';
import { ProfileComponent } from './main/user/profile.component';
import { UserPrefComponent } from './main/user/user-pref.component';
import { FaqComponent } from './main/user/faq.component';
import { MultiOrderComponent } from './main/robinhood/multi-order.component';
import { AuthenticateComponent } from './main/td/authenticate.component';

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
        path     : 'td/:broker',
        component: TDOrderComponent,
        canActivate: [AuthGuard]
    },
    {
        path     : 'tdlogin',
        component: AuthenticateComponent,
        canActivate: [AuthGuard]
    },
    {
        path     : 'user',
        component: UserComponent,
        canActivate: [AuthGuard]
    },
    {
        path     : 'multi',
        component: MultiOrderComponent,
        canActivate: [AuthGuard]
    },

    {
        path     : 'pie',
        component: PieComponent,
        canActivate: [AuthGuard]
    },

    {
        path     : 'position',
        component: PositionComponent,
        canActivate: [AuthGuard]
    },

    {
        path     : 'buyback',
        component: BuybackComponent,
        canActivate: [AuthGuard]
    },

    {
        path     : 'password',
        component: ChangePasswordComponent,
        canActivate: [AuthGuard]
    },
    {
        path     : 'pref',
        component: UserPrefComponent,
        canActivate: [AuthGuard]
    },
    {
        path     : 'faq',
        component: FaqComponent,
        canActivate: [AuthGuard]
    },
    {
        path     : 'profile',
        component: ProfileComponent,
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
        AppHeaderComponent,
        UserComponent,
        PieComponent,
        PieDetailComponent,
        PositionComponent,
        BuybackComponent,
        ChangePasswordComponent,
        ProfileComponent,
        UserPrefComponent,
        FaqComponent,
        MultiOrderComponent,
        AuthenticateComponent
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
        TableModule,
        AgGridModule.withComponents([])
    ],
    exports: [OrderComponent]
})
export class MyAppModule
{
}
