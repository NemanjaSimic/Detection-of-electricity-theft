import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import {CSVRecord} from '../../models/CSVModel';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EventEmitter } from 'protractor';
import { EventEmitterService } from 'src/app/services/event-emitter.service';
import {consumers} from '../../models/data,mock';

@Component({
  selector: 'app-load-file',
  templateUrl: './load-file.component.html',
  styleUrls: ['./load-file.component.css']
})
export class LoadFileComponent implements OnInit {

httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Autorization': 'my-auth-token'
  })
};

  public records: any[] = [];
  @ViewChild('csvReader', {static: false}) csvReader: any;

  constructor(private http: HttpClient, private eventEmitterService: EventEmitterService) {

   }

  ngOnInit() {
    this.records = consumers;
  }

  uploadListener($event: any): void {

    let text = [];
    let files = $event.srcElement.files;

    if (this.isValidCSVFile(files[0])) {

      let input = $event.target;
      let reader = new FileReader();
      reader.readAsText(input.files[0]);

      reader.onload = () => {
        let csvData = reader.result;
        let csvRecordsArray = (<string>csvData).split(/\r\n|\n/);

        let headersRow = this.getHeaderArray(csvRecordsArray);

        this.records = this.getDataRecordsArrayFromCSVFile(csvRecordsArray, headersRow.length);
      };

      reader.onerror = function () {
        console.log('error is occured while reading file!');
      };

    } else {
      alert("Please import valid .csv file.");
      this.fileReset();
    }
  }

  getDataRecordsArrayFromCSVFile(csvRecordsArray: any, headerLength: any) {
    let csvArr = [];

    for (let i = 1; i < csvRecordsArray.length; i++) {
      let curruntRecord = (<string>csvRecordsArray[i]).split(',');
      if (curruntRecord.length == headerLength) {
        let csvRecord: CSVRecord = new CSVRecord();
        csvRecord.name = curruntRecord[0].trim();
        csvRecord.time = curruntRecord[1].trim();
        csvRecord.consuption = curruntRecord[2].trim();
        csvArr.push(csvRecord);
      }
    }
    return csvArr;
  }

  isValidCSVFile(file: any) {
    return file.name.endsWith(".csv");
  }

  getHeaderArray(csvRecordsArr: any) {
    let headers = (<string>csvRecordsArr[0]).split(',');
    let headerArray = [];
    for (let j = 0; j < headers.length; j++) {
      headerArray.push(headers[j]);
    }
    return headerArray;
  }

  fileReset() {
    this.csvReader.nativeElement.value = "";
    this.records = [];
  }

  public predict(){
      this.http.post<any>('https://localhost:44306/api/csv',this.records, this.httpOptions).subscribe(
        data =>
        {
          console.log(data);
        }
      )
  }

  public onClick(index: any){
    console.log(this.records[index]);
    this.eventEmitterService.onGraphInvoke(this.records[index]);
    this.eventEmitterService.onMapInvoke(this.records[index]);
  }
}
