import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { TransactionClient } from '../../../client/transaction.client';
import { TransactionDescriptions } from '../../../models/transactionDescriptions';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrl: './summary.component.css'
})
export class SummaryComponent {
  public transactionDescriptionNames: TransactionDescriptions[] = [];
  public transactions: any[] = [];
  public total = 0;
  public pageNumber = 1;
  public pageSize = 100;
  public sortBy = 'transactionDate'; // Default sort field
  public isDescending = true; // Default sort order
  public filters = {
    transactionAmount: null,
    transactionType: null,
    transactionDescription: null,
  };
  
  constructor(
    private transactionClient: TransactionClient,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadTransactionDescriptionNames();
    this.getTransactions();
  }

  exportTableToPDF() {
    const table = document.getElementById('contentToConvert'); // ID tabele ili sadržaja
    
    html2canvas(table!).then((canvas) => {
      const imgWidth = 208; 
      const pageHeight = 295; 
      const imgHeight = (canvas.height * imgWidth) / canvas.width;
      const imgData = canvas.toDataURL('image/png');
      const pdf = new jsPDF('p', 'mm', 'a4');
      let position = 0;

      pdf.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight);
      pdf.save('table.pdf'); // Ime PDF dokumenta
    });

    
  }

  
  private loadTransactionDescriptionNames(): void {
    this.transactionClient.getTransactionDescriptionNames().subscribe(
      (descriptions: TransactionDescriptions[]) => {
        this.transactionDescriptionNames = descriptions;
      },
      (error) => {
        console.error('Error fetching transaction descriptions:', error);
      }
    );
  }


  private getTransactions(): void {
    this.transactionClient.getTransactionData(this.pageNumber, this.pageSize, this.sortBy, this.isDescending, this.filters).subscribe(
      (res) => {
        this.transactions = res.page.data;
        this.total = res.page.total;
      },
      (error) => {
        console.error('Error fetching transactions:', error);
      }
    );
  }

  public getDescriptionName(id: number): string {
    const description = this.transactionDescriptionNames.find(desc => desc.id === id);
    return description ? description.descriptionName : 'N/A';
  }
}