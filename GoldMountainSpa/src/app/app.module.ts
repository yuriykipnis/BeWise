import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from './shared/shared.module';
import { AppComponent } from './app.component';
import { routes } from "./index";
import { MainComponent } from './views/main/main.component';
import { HeaderComponent } from './views/header/header.component';
import { AccountsComponent } from './accounts/accounts.component';
import { InsuranceComponent } from './insurance/insurance.component';
import { NewAccountComponent } from './accounts/new-account/new-account.component';
import { StoreModule } from '@ngrx/store';
import { BankService } from "./services/bank.service";
import { ProviderService } from "./services/provider.service";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SeInsurService } from "./insurance/services/se-insur.service";
import { UserProfileService } from "./services/user-profile.service";
import { CreditService } from "./services/credit.service";
import { LoanService } from "./loans/services/loan.service";
import { InstitutionService } from "./services/institution.service";
import { StudyFundService } from "./insurance/services/study-fund.service";
import { PensionFundService } from "./insurance/services/pension-fund.service";
import {MortgageInsurService} from "./insurance/services/mortgage-insur.service";
import {ProvidentFundService} from "./insurance/services/provident-fund.service";
import {AuthService} from "./auth/auth.service";
import {AuthGuard} from "./auth/auth.guard";
import {LoginCallbackComponent} from "./auth/login-callback/login-callback.component";
import {PlanningComponent} from "./planning/planning.component";
import { AccountsSidebarComponent } from './accounts/accounts-sidebar/accounts-sidebar.component';
import { InsurSidebarComponent } from './insurance/insur-sidebar/insur-sidebar.component';
import { SidebarBankAccountComponent } from './accounts/accounts-sidebar/sidebar-bank-account/sidebar-bank-account.component';
import { SidebarCreditAccountComponent } from './accounts/accounts-sidebar/sidebar-credit-account/sidebar-credit-account.component';
import { OverviewComponent } from './overview/overview.component';
import { SidebarInsurProfileComponent } from './insurance/insur-sidebar/sidebar-insur-profile/sidebar-insur-profile.component';
import { TransactionsViewComponent } from './accounts/transactions-view/transactions-view.component';
import { StudyFundViewComponent } from './insurance/views/study-fund-view/study-fund-view.component';
import { SeInsurViewComponent } from './insurance/views/se-insur-view/se-insur-view.component';
import { PensionFundViewComponent } from './insurance/views/pension-fund-view/pension-fund-view.component';
import { ProvidentFundViewComponent } from './insurance/views/provident-fund-view/provident-fund-view.component';
import { MortgageInsurViewComponent } from './insurance/views/mortgage-insur-view/mortgage-insur-view.component';
import { SummaryComponent } from './insurance/views/summary/summary.component';
import {AccountsSummaryService} from "./accounts/services/accounts-summary.service";
import { LoansSidebarComponent } from './loans/loans-sidebar/loans-sidebar.component';
import { LoanViewComponent } from './loans/loan-view/loan-view.component';
import { LoansComponent } from './loans/loans.component';
import { LoansSummaryComponent } from './loans/loans-summary/loans-summary.component';
import { CommandBarComponent } from './accounts/command-bar/command-bar.component';
import { StatusSidebarComponent } from './accounts/status-sidebar/status-sidebar.component';
import {AccountControlService} from './accounts/services/account-control.service';
import { LoansStatusBarComponent } from './loans/loans-status-bar/loans-status-bar.component';
import {LoanControlService} from './loans/services/loan-control.service';
import { ContactUsComponent } from './contact-us/contact-us.component';
import {ContactService} from "./services/contact.service";
import {OverviewService} from './services/overview.service';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    HeaderComponent,
    AccountsComponent,
    PlanningComponent,
    InsuranceComponent,
    NewAccountComponent,
    LoginCallbackComponent,
    AccountsSidebarComponent,
    InsurSidebarComponent,
    SidebarBankAccountComponent,
    SidebarCreditAccountComponent,
    OverviewComponent,
    SidebarInsurProfileComponent,
    TransactionsViewComponent,
    StudyFundViewComponent,
    SeInsurViewComponent,
    PensionFundViewComponent,
    ProvidentFundViewComponent,
    MortgageInsurViewComponent,
    SummaryComponent,
    LoansSidebarComponent,
    LoanViewComponent,
    LoansComponent,
    LoansSummaryComponent,
    CommandBarComponent,
    StatusSidebarComponent,
    LoansStatusBarComponent,
    ContactUsComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    SharedModule.forRoot(),
    NgbModule.forRoot()
  ],
  providers: [
    AuthGuard,
    AuthService,
    InstitutionService,
    BankService,
    CreditService,
    LoanService,
    SeInsurService,
    PensionFundService,
    StudyFundService,
    MortgageInsurService,
    ProvidentFundService,
    ProviderService,
    UserProfileService,
    AccountsSummaryService,
    AccountControlService,
    LoanControlService,
    ContactService,
    OverviewService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {

}
