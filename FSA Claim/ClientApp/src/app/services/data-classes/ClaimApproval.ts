import { IClaimApproval } from "../interfaces/IApproveClaim";



export class ClaimApproval implements IClaimApproval {
  approve: boolean = true;
  referenceNumber: string = "";

}
