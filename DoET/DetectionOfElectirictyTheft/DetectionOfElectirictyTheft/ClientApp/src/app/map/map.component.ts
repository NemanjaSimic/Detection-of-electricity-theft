import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import * as L from 'leaflet';
import 'mapbox-gl-leaflet';
import { ProcessingResult } from '../models/CSVModel';
import { EventEmitterService } from '../services/event-emitter.service';
import {MarkerService} from '../services/marker.service';

const iconRetinaUrl = 'assets/marker-icon-2x.png';
const iconUrl = 'assets/marker-icon.png';
const shadowUrl = 'assets/marker-shadow.png';
const iconDefault = L.icon({
  iconRetinaUrl,
  iconUrl,
  shadowUrl,
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  tooltipAnchor: [16, -28],
  shadowSize: [41, 41]
});
L.Marker.prototype.options.icon = iconDefault;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements AfterViewInit {

  private map: L.Map;
  @ViewChild('map', {static: true})
  private mapContainer: ElementRef<HTMLElement>;
  processResults: ProcessingResult[] = [];
  show: boolean = false;
  currentMarker;
  constructor(private eventEmitterService: EventEmitterService) {

   }



  ngAfterViewInit() {

    if (this.eventEmitterService.subsMap==undefined) {    
      this.eventEmitterService.subsMap = this.eventEmitterService.    
      invokeMap.subscribe((data:any) => {    
        this.update(data);    
      });    
    }    
    this.initMap();
   //this.placeMarker(45.239233, 19.835452);
  }

  private initMap(): void {
    this.map = L.map('map', {
      center: [ 45.267136, 19.833549 ],
      zoom: 14
    });

    const tiles = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',  {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    });

    tiles.addTo(this.map);
 
  }

  placeMarker(data :any){
    this.currentMarker = L.marker([data.coordinates[0], data.coordinates[1]]).addTo(this.map)
    .bindPopup(`${data.streetNameNum[0]}, ${data.streetNameNum[1]}`)
    .openPopup();

    this.map.panTo(new L.LatLng(data.coordinates[0], data.coordinates[1]));
  }

  update(data:any){
    console.log("from map:");
    console.log(data);
    if(this.show == false)
    {
        this.show = true;
        
    }
    if(this.currentMarker != undefined)
    {
      this.map.removeLayer(this.currentMarker);
    }
    this.placeMarker(data);

  }
  
}
