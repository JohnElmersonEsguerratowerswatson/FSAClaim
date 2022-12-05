import { IViewClaim } from "./IViewClaim";

export interface IGetClaimResult {

  claims: IViewClaim[];
  fsaAmount: number;
  yearCoverage: number;
  employeeID: number;
  employeeName: string;
  availableFSA: number;
  pendingClaims: number;
  approvedClaims: number;

  /*
        public IEnumerable<IViewClaim> Claims { get; set; }
        public Decimal FSAAmount { get; set; }

        public int YearCoverage { get; set; }

        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public Decimal AvailableFSA { get; set; }

        public Decimal PendingClaims { get; set; }

        public Decimal ApprovedClaims { get; set; }
*/
}
