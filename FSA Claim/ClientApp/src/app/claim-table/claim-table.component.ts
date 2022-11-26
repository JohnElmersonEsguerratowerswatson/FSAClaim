import { Component, OnDestroy, OnInit } from "@angular/core";
import { Route, Router } from "@angular/router";
import { Subscription } from "rxjs";
import { ViewClaim } from "../services/data-classes/ViewClaim";
import { ClaimListService } from "../services/claim-list.service";
import { IGetClaimResult } from "../services/interfaces/IGetClaimsResult";
import { IViewClaim } from "../services/interfaces/IViewClaim";

@Component({
  selector: 'app-claims-component',
  templateUrl: 'claim-table.component.html'
})
export class ClaimTableComponent implements OnInit, OnDestroy {

  public claims: ViewClaim[] = [];
  public claimsView: IGetClaimResult;

  private subscription: any;
  private bearer: any;

  get claimsFromAPI(): ViewClaim[] {
    return this.claims;
  }

  set claimsFromAPI(claimsArray: ViewClaim[]) {
    //this.claims.concat(claimsArray);
    //console.log(JSON.stringify(claimsArray));
    for (var i = 0; i < claimsArray.length; i++) {
      this.claims.push(claimsArray[i]);
      console.log(JSON.stringify(claimsArray[i]));
    }
  }

  constructor(private claimService: ClaimListService, private router: Router) {
    this.claimsView = {} as IGetClaimResult;
    this.bearer = this.router.getCurrentNavigation()?.extras.state;
    this.subscription = this.claimService.getClaimsAPI().subscribe(
      result => {
        console.log(JSON.stringify(result));
        this.claimsView = result;
      },
      err => { alert("There was a problem fetching your claims."); }
    );

    //this.claims.push({
    //  DateSubmitted: "11/23/2022",
    //  Status: 'Pending',
    //  ClaimAmount: 200,
    //  ReceiptAmount: 4343.23,
    //  TotalClaimAmount: 2000,
    //  ReceiptDate: "09/24/2022",
    //  ReceiptNumber: "98LKRV532434",
    //  ReferenceNumber: "0923498LKRV532434"
    //} as IViewClaim);

    //this.claims.push({
    //  DateSubmitted : "11/24/2022",
    //  Status : 'Pending',
    //  ClaimAmount : 2000,
    //  ReceiptAmount : 3232.23,
    //  TotalClaimAmount : 2000,
    //  ReceiptDate: "09/23/2022",
    //  ReceiptNumber : "4893NB98UG4R",
    //  ReferenceNumber : "09234893NB98UG4R"
    //} as IViewClaim);

    //this.bearer = this.router.getCurrentNavigation()?.extras.state;
    ////console.log(this.bearer);
    //this.subscription = this.claimService.getClaimsAPI(this.bearer).subscribe(
    //  claimResult => { this.claims.concat(claimResult); },
    //  errorResult => { alert("There was a problem fetching your claims."); });


  }
  ngOnDestroy(): void {
    // this.subscription.unsubscribe();
  }
  ngOnInit(): void {

    //throw new Error("Method not implemented.");
  }
  //  claimResult => { this.claims.concat(claimResult); },
  //errorResult => { alert("There was a problem fetching your claims."); });
}
