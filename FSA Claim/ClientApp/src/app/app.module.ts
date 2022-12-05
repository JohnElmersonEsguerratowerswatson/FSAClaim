import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ClaimTableComponent } from './claim-table/claim-table.component';
import { ClaimDetailComponent } from './claim-detail/claim-detail.component';
import { ClaimConstructComponent } from './claim-construct/claim-construct.component';
import { LoginComponent } from './login/login.component';
import { LoggedOutNotificationComponent } from './shared/logged-out-notification.component';
import { FSARuleComponent } from './fsa-rule-admin/fsa-rule.component';
import { ClaimApprovalComponent } from './claim-approval-admin/claim-approval.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ClaimTableComponent,
    ClaimDetailComponent,
    ClaimConstructComponent,
    LoginComponent,
    LoggedOutNotificationComponent,
    FSARuleComponent,
    ClaimApprovalComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: ClaimTableComponent, pathMatch: 'full' },
      { path: 'claim-list', component: ClaimTableComponent },
      //{ path: 'login', component: LoginComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'claim-detail', component: ClaimDetailComponent },
      { path: 'claim-data', component: ClaimConstructComponent },
      { path: 'fsa-rule', component: FSARuleComponent },
      { path: 'fsa-approval', component: ClaimApprovalComponent },
      { path: 'fetch-data', component: FetchDataComponent },


    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
