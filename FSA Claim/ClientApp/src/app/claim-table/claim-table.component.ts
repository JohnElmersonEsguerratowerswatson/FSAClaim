import { Component } from "@angular/core";
import { IViewClaim } from "../services/interfaces/IViewClaim";

@Component({
  selector: 'app-claims-component',
  templateUrl: 'claim-table.component.html'
})
export class ClaimTableComponent {
  public claims: IViewClaim[];

  constructor() {
    this.claims = [];

    this.claims.push({
      DateSubmitted: "11/23/2022",
      Status: 'Pending',
      ClaimAmount: 200,
      ReceiptAmount: 4343.23,
      TotalClaimAmount: 2000,
      ReceiptDate: "09/24/2022",
      ReceiptNumber: "98LKRV532434",
      ReferenceNumber: "0923498LKRV532434"
    } as IViewClaim);

    this.claims.push({
      DateSubmitted : "11/24/2022",
      Status : 'Pending',
      ClaimAmount : 2000,
      ReceiptAmount : 3232.23,
      TotalClaimAmount : 2000,
      ReceiptDate: "09/23/2022",
      ReceiptNumber : "4893NB98UG4R",
      ReferenceNumber : "09234893NB98UG4R"
    } as IViewClaim);
  }
}
