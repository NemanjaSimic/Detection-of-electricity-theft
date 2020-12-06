import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ProcessingResult } from '../models/CSVModel';
import { EventEmitterService } from '../services/event-emitter.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{
  isExpanded = false;
  processResults : ProcessingResult[];
  constructor(private http: HttpClient, private eventEmitterService: EventEmitterService) {

  }

  ngOnInit(){

  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  aquire(){
    let result = this.http.get<ProcessingResult[]>('https://localhost:44306/api/csv').subscribe
    (result => {
      this.processResults = result;
      /* this.eventEmitterService.onGraphInvoke(this.processResults);
      this.eventEmitterService.onMapInvoke(this.processResults); */
      this.eventEmitterService.onTableInvoke(this.processResults);
      console.log(this.processResults);
    });

  }

  import(){
    let result = this.http.get<ProcessingResult>('https://localhost:44306/api/csv') as Observable<ProcessingResult>;


  }
}
