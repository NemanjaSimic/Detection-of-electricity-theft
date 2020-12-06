import { Component, OnInit } from '@angular/core';
import { ProcessingResult } from '../models/CSVModel';
import { data } from '../models/data,mock';
import { EventEmitterService } from '../services/event-emitter.service';
import * as CanvasJS from './canvasjs.min';
//var CanvasJS = require('canvasjs');

@Component({
  selector: 'app-graph',
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.css']
})
export class GraphComponent implements OnInit {
private chart;
private avg: any;
  constructor(private eventEmitterService: EventEmitterService) { }
public processResults : ProcessingResult[] = [];
  ngOnInit() {
    if (this.eventEmitterService.subsVar==undefined) {    
      this.eventEmitterService.subsVar = this.eventEmitterService.    
      invokeGraph.subscribe((data:any) => {    
        this.update(data);    
	  });    
	}    
	
	if (this.eventEmitterService.subsAvg==undefined) {    
		this.eventEmitterService.subsAvg = this.eventEmitterService.    
		provideAvg.subscribe((data:any) => {    
		  this.provideAvg(data);    
		});    
	  }   

    let dataPoints = [
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
    ];
    
    let dataPoints2 = [
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		];
		
		this.chart = new CanvasJS.Chart("chartContainer",{
			animationEnabled: true,
			data: [{
					type: "line",
					dataPoints : dataPoints,
					showInLegend : true,
					legendText : "Average"  
      			},
   				{
      				type: "line",
					dataPoints : dataPoints,
					showInLegend : true,
					legendText : "Consumer"  
    			}]
		});

		this.chart.render(); 
      
  }
	provideAvg(data: any) {
		this.avg = data;

		
	let newPoints = [];
	data.forEach(element => {
		newPoints.push({y: element})
	});
	
	this.chart.options.data[0].dataPoints = newPoints
	let dataPoints2 = [
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		{ y: 0 },
		];

	this.chart.options.data[1].dataPoints = dataPoints2
	this.chart.render();
	}

  update(newData:any){
    console.log("from graph:");
	console.log(newData);
	this.processResults = newData;

	let newPoints = [];
	newData.valuesByHour.forEach(element => {
		newPoints.push({y: element})
	});
	
	this.chart.options.data[1].dataPoints = newPoints
	this.chart.render();
  }
}
