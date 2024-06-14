export interface AutomaticTransaction {
    id: number;
    transactionAmount: number;
    transactionType: boolean;
    transactionDescription: number;
    transactionDate: Date;
    frequency: FrequencyType;
    nextExecutionDate: Date;
    insertedDate: Date;
  }
  
  export enum FrequencyType {
    Weekly,
    Monthly,
    Yearly
  }
  