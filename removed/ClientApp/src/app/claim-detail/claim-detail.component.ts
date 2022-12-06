import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription } from "rxjs";
import { ClaimDeleteService } from "../services/claim-delete.service";
import { IViewClaim } from "../services/interfaces/IViewClaim";

@Component({
  selector: 'app-claim-const-component',
  templateUrl: 'claim-detail.component.html'
})
export class ClaimDetailComponent implements OnInit {

  public claim: IViewClaim;
  private subscription: Subscription;
  constructor(private router: Router, private route: ActivatedRoute,private claimDeleteService: ClaimDeleteService) {
    //this.claim = {} as IViewClaim;
    this.subscription = new Subscription();
    this.claim = this.router.getCurrentNavigation()?.extras.state as IViewClaim;
  }

  ngOnInit(): void {

  }

  public editClaim() {
    this.router.navigateByUrl('/claim-data', {
      state: this.claim
    });
  }

  public deleteClaim() {
    this.subscription = this.claimDeleteService.claimDeleteAPI(this.claim).subscribe(
      result => {
        alert("Successfully deleted claim.");
        this.router.navigateByUrl("claim-list");
      },
      errorResult => {
        alert("Failed to delete claim.");
        
      }
    );

  }

}
