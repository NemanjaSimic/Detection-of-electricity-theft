export class CSVRecord {
    public name: any;
    public time: any;
    public consuption: any;
  }

  export class Consumer{
    public name: any;
    public surname: any;
    public consumption: any;
  }

  export class ProcessingResult{
    public valuesByHour: [];
    public coordinates: [];
    public streetNameNum: [];
    public isAvg: boolean;
  }