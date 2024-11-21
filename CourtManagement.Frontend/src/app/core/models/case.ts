export interface Case {
    id: number;
    caseNumber: string;
    title: string;
    status: number;
    statusDsc: string;
    judgeId: string;
    judgeName: string;
    openDate: Date;
}
  export interface CaseFilter {
    judgeId: string | null;
    status: number | null;
    sortBy: string;
    sortDirection: string;
}