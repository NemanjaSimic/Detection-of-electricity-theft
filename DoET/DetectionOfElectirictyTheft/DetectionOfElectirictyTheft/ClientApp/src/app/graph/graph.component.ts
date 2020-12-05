import { Component, OnInit } from '@angular/core';
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
  constructor(private eventEmitterService: EventEmitterService) { }

  ngOnInit() {
    if (this.eventEmitterService.subsVar==undefined) {    
      this.eventEmitterService.subsVar = this.eventEmitterService.    
      invokeGraph.subscribe((data:any) => {    
        this.update(data);    
      });    
    }    
    let dataPoints = [
			{ y: 71 },
			{ y: 55 },
			{ y: 50 },
			{ y: 65 },
			{ y: 95 },
			{ y: 68 },
			{ y: 28 },
			{ y: 34 },
			{ y: 14 }
    ];
    
    let dataPoints2 = [
			{ y: 81 },
			{ y: 65 },
			{ y: 60 },
			{ y: 85 },
			{ y: 105 },
			{ y: 78 },
			{ y: 38 },
			{ y: 44 },
			{ y: 34 }
		];
		
		this.chart = new CanvasJS.Chart("chartContainer",{
			animationEnabled: true,
			title:{
				text: "Statistika potrosaca"
			},
			data: [{
				type: "line",
				dataPoints : dataPoints
      },
    {
      type: "line",
      dataPoints : dataPoints2
    }]
		});
		this.chart.render();
      
  }

  update(data:any){
    console.log("from graph:");
    console.log(data);
  }
}
