import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { TransactClaim } from "../services/data-classes/TransactClaim";
import { ClaimConstructService } from "../services/claim-construct.service";
import { ITransactClaim } from "../services/interfaces/ITransactClaim";
import { ITransactClaimResult } from "../services/interfaces/ITransactClaimResult";

@Component({
  selector: 'app-claim-const-component',
  templateUrl: 'claim-construct.component.html'
})

export class ClaimConstructComponent implements OnInit {

  public claim: TransactClaim ;
  public subscription: Subscription;
  public claimResult: ITransactClaimResult;
  constructor(private router: Router, private claimConstructService: ClaimConstructService) {

    this.claimResult = {} as ITransactClaimResult;
    //this.claim = new TransactClaim();
    //this.claim.referenceNumber = "";
    this.subscription = {} as Subscription;

    this.claim = this.router.getCurrentNavigation()?.extras.state as ITransactClaim;
    if (this.claim == null) this.claim = new TransactClaim();
  }


  onSubmit(): void {
    let isNew: boolean = this.claim.referenceNumber.length <= 0;
    this.subscription = this.claimConstructService.claimConstructAPI(isNew, this.claim).subscribe(
      result => {
        console.log(JSON.stringify(result));
        this.claimResult = result as ITransactClaimResult;
        alert("Successfull submitted claim for approval.");
        this.router.navigateByUrl("claim-list");
      },
      err => { alert("There was a problem fetching your claims."); }
    );

  }

  ngOnInit(): void {


  }

}
