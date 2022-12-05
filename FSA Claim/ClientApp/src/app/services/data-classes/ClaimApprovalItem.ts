import { IClaimApprovalItem } from "../interfaces/IClaimApprovalItem";
import { IViewClaim } from "../interfaces/IViewClaim";

export class ClaimApprovalItem implements IViewClaim, IClaimApprovalItem {

  public employeeID: number = 0;
  public employeeName: string = "";
  public referenceNumber: string = "";
  public claimAmount: number = 0;
  public receiptNumber: string = "";
  public receiptAmount: number = 0;
  public receiptDate: string = "";
  public dateSubmitted: string = "";
  public status: string = "";
  public totalClaimAmount: number = 0;

}
