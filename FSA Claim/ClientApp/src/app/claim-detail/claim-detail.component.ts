import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { IViewClaim } from "../services/interfaces/IViewClaim";

@Component({
  selector: 'app-claim-const-component',
  templateUrl: 'claim-detail.component.html'
})
export class ClaimDetailComponent implements OnInit {
  public claim: IViewClaim;

  constructor(private router: Router, private route: ActivatedRoute) {
    //this.claim = {} as IViewClaim;
    this.claim = this.router.getCurrentNavigation()?.extras.state as IViewClaim;
  }
  ngOnInit(): void {

  }


  public editClaim() {
    this.router.navigateByUrl('/claim-data', {
      state: this.claim
    });
  }

  public deleteClaim() { }
}
