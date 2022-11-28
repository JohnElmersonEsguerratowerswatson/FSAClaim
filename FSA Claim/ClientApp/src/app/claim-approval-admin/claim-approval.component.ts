import { Component, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { ClaimApprovalService } from "../services/claim-approval.service";
import { ClaimApproval } from "../services/data-classes/ClaimApproval";
import { ClaimApprovalItem } from "../services/data-classes/ClaimApprovalItem";

@Component({
  selector: "app-claim-approval-component",
  templateUrl: "claim-approval.component.html"
})
export class ClaimApprovalComponent implements OnInit, OnDestroy {

  public claims: ClaimApprovalItem[];
  private subscription: Subscription;
  constructor(private service: ClaimApprovalService,  private router: Router) {
    this.subscription = this.service.claimTableAPI().subscribe(
      result => { this.claims = result as ClaimApprovalItem[]; },
      errorResult => {
        alert("Failed to load claims");
      }
    );
    this.claims = [];
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
  ngOnInit(): void {
    //INITIALIZE code executes after constructor code
  }


  onApproveClick(referenceNumber: string): void {
   // alert(referenceNumber);
    let claimApprovalParam = new ClaimApproval()
    claimApprovalParam.approve = true;
    claimApprovalParam.referenceNumber = referenceNumber;
    this.subscription.unsubscribe();
    this.subscription = this.service.claimApprovalAPI(claimApprovalParam).subscribe(
      result => { alert("Successfully approved claim."); },
      errorResult => { alert("Failed to approve claim."); }
    );
  }

  onDenyClick(referenceNumber: string): void {
    let claimApprovalParam = new ClaimApproval()
    claimApprovalParam.approve = false;
    claimApprovalParam.referenceNumber = referenceNumber;
  
    this.subscription.unsubscribe();
    this.subscription = this.service.claimApprovalAPI(claimApprovalParam).subscribe(
      result => { alert("Successfully denied claim."); },
      errorResult => { alert("Failed to deny claim."); }
    );
  }
}
