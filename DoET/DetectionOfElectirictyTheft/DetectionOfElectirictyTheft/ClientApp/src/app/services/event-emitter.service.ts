import { Injectable, EventEmitter } from '@angular/core';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventEmitterService {

  invokeGraph = new EventEmitter();
  subsVar: Subscription;

  invokeMap = new EventEmitter();
  subsMap: Subscription;
  constructor() { }

  public onGraphInvoke(data: any){
    this.invokeGraph.emit(data);
  }

  public onMapInvoke(data:any){
    this.invokeMap.emit(data);
  }
}
