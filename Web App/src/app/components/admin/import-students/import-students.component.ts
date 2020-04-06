import { Component, OnInit, ViewChild } from '@angular/core';
import { BroadCasterService } from '../../../services/broad-caster.service';
import { CommonService, SessionService } from '../../../services';
import { SchoolService } from '../../../services/school.service';
import { OrderService } from '../../../services/order.service';
declare var $: any;
@Component({
  selector: 'app-import-students',
  templateUrl: './import-students.component.html',
  styleUrls: ['./import-students.component.scss']
})
export class ImportStudentsComponent implements OnInit {
  @ViewChild('csvReader', { static: true }) csvReader: any;
  public records: any[] = [];
  public invalidRows: any = [];
  schoolsList: any = [];
  selectedSchool: any = '';
  constructor(
    private orderService: OrderService,
    private commonService: CommonService,
    private schoolService: SchoolService,
    private sessionService: SessionService,
    private broadcasterService: BroadCasterService,
  ) { }

  ngOnInit() {
    this.broadcasterService.broadcast("PageTitle", "Import Students");
    this.getSchoolsList();
  }

  getSchoolsList() {
    this.schoolService.getSchoolDropdown()
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.schoolsList = response.Data;
        })
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
        console.log(this.records);
        if (this.records.length == 0) {
          this.commonService.showErrorMessage('Records not found in selected file.');
          return;
        }
        this.invalidRows = this.records.filter(a => a.ErrorMessage);
        if (this.invalidRows.length > 0) {
          this.commonService.showErrorMessage('Some row has errors please correct them and try again.');
          this.records = [];
          this.csvReader.nativeElement.value = "";
          $('#InvalidRecordsPopUp').modal('show');
          return;
        }
      };

      reader.onerror = function () {
        console.log('error is occured while reading file!');
      };

    } else {
      this.commonService.showErrorMessage("Please import valid .csv file.");
      this.fileReset();
    }
  }

  getDataRecordsArrayFromCSVFile(csvRecordsArray: any, headerLength: any) {
    let csvArr = [];

    for (let i = 1; i < csvRecordsArray.length; i++) {
      let curruntRecord = (<string>csvRecordsArray[i]).split(',');
      if ((curruntRecord.length == headerLength || curruntRecord.length > 0) && curruntRecord[1]) {
        let csvRecord: CSVRecord = new CSVRecord();
        csvRecord.SerialNo = this.trimText(curruntRecord[0]);
        csvRecord.StudentName = this.trimText(curruntRecord[1]);
        csvRecord.ParentsName = this.trimText(curruntRecord[2]);
        csvRecord.Class = this.trimText(curruntRecord[3]);
        csvRecord.EmailId = this.trimText(curruntRecord[4]);
        csvRecord.Address = this.trimText(curruntRecord[5]);
        csvRecord.City = this.trimText(curruntRecord[6]);
        csvRecord.State = this.trimText(curruntRecord[7]);
        csvRecord.MobileNo = this.trimText(curruntRecord[8]);
        csvRecord.EnrollmentNo = this.trimText(curruntRecord[9]);
        csvRecord.DOB = this.trimText(curruntRecord[10]);
        this.validateCSVFileRow(csvRecord);
        csvArr.push(csvRecord);
      }
    }
    return csvArr;
  }

  trimText(text: any) {
    if (text) {
      return text.trim();
    }
    return text;
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

  validateCSVFileRow(rowData: CSVRecord) {
    let errorMessage = "";
    if (!rowData) {
      errorMessage += "Invalid details in row.";
      return;
    }
    //validate serial no
    if (!rowData.SerialNo) {
      errorMessage += " Serial no is required.";
    }
    else {
      var isDuplicateSNo = this.records.filter(a => a.SerialNo = rowData.SerialNo);
      if (isDuplicateSNo.length > 0) {
        errorMessage += " Serial no already exists";
      }
    }
    //validate student name
    if (!rowData.StudentName) {
      errorMessage += " Student name is required.";
    }
    //validate parent's name
    if (!rowData.ParentsName) {
      errorMessage += " Parents name is required.";
    }
    //validate class
    if (!rowData.Class) {
      errorMessage += " Class is required.";
    }
    //check valid class
    else {
      if (!this.isValidClass(rowData.Class)) {
        errorMessage += " Invalid class.";
      }
    }
    //validate mobile no
    if (!rowData.MobileNo) {
      errorMessage += " Mobile no is required.";
    }
    // check valid mobile no
    else {
      if (!this.isPhoneNumber(rowData.MobileNo)) {
        errorMessage += " Invalid mobile number.";
      }
    }
    rowData.ErrorMessage = errorMessage;
  }

  isValidClass(inputClass) {
    return true;
  }
  isPhoneNumber(inputtxt) {
    var phoneno = /^\d{10}$/;
    if (inputtxt.match(phoneno)) {
      return true;
    }
    else {
      return false;
    }
  }

  //
  onSubmitClick() {
    if (!this.selectedSchool) {
      this.commonService.showErrorMessage("Please select school.");
      return;
    }
    if (!this.records || this.records.length == 0) {
      this.commonService.showErrorMessage("Please please select valid file and try again.");
      return;
    }
    var data = {
      SchoolId: this.selectedSchool,
      Students: this.records
    }
    this.commonService.showSpinner();
    this.orderService.imporStudents(data)
      .subscribe(
        response => {
          debugger;
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }

          this.commonService.showSuccessMessage(response.Data);
          //this.router.navigate(["/admin/book-seller-mapping"]);
        })

  }
  onResetClick() {
    this.selectedSchool = '';
    this.csvReader.nativeElement.value = "";
    this.records = [];
  }
}

export class CSVRecord {
  public SerialNo: any;
  public StudentName: any;
  public ParentsName: any;
  public Class: any;
  public EmailId: any;
  public Address: any;
  public City: any;
  public State: any;
  public MobileNo: any;
  public EnrollmentNo: any;
  public DOB: any;
  public ErrorMessage: any;
}  
