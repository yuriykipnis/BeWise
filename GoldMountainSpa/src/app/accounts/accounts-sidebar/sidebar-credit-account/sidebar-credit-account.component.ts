import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import {CreditAccount} from "../../models/credit-account";
import {TransactionType} from "../../../models/transaction";
import {AccountControlService} from "../../services/account-control.service";

@Component({
  selector: 'app-sidebar-credit-account',
  templateUrl: './sidebar-credit-account.component.html',
  styleUrls: ['./sidebar-credit-account.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SidebarCreditAccountComponent implements OnInit {
  @Input() account: CreditAccount;
  constructor(private accountControlService: AccountControlService) {
  }

  ngOnInit() {
  }

  isSelected() {
    let selectedAccount = this.accountControlService.getSelectedAccount();
    let selectedID = selectedAccount != null ? selectedAccount.Id : -1;
    return this.account.Id == selectedID;
  }

  getTotalCharge(account){
    let result: number = 0;
    let now = new Date();
    account.Transactions.filter(t => {
      let tDate = new Date(t.PaymentDate);
      return tDate.getMonth() === now.getMonth() && tDate.getFullYear() === now.getFullYear();
    }).forEach(t => {
      result += (t.Type === TransactionType.Income) ? t.Amount : 0;
      result -= (t.Type === TransactionType.Expense) ? t.Amount : 0;
    });

    return result;
  }
}
