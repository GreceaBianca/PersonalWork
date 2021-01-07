import { Component, ViewChild, Input, Output, EventEmitter, ElementRef } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';

@Component({
  selector: 'rent-file-uploader',
  templateUrl: './file-uploader.component.html',
  styleUrls: ['./file-uploader.component.scss']
})
export class FileUploaderComponent {

  @Output() onFileSelected = new EventEmitter<File>();
  @ViewChild('fileInput', null) fileInput: ElementRef;

  //if other file type uploads are required set bellow params as inputs
  mimeType = 'image/*';
  maxFileSize = 5 * 1024 * 1024; //5MB

  file: File;
  hasDropZoneOver: boolean = false;

  uploader: FileUploader = new FileUploader({
    allowedMimeType: [this.mimeType],
    maxFileSize: this.maxFileSize
  });
  errors: string[] =[];

  public onChange() {
    this.fileInput.nativeElement.value = '';
  }

  public fileOverBase(e: any): void {
    this.hasDropZoneOver = e;
  }

  public onFileDrop(e) {
    this.file = e[0];
    this.errors = [];

    if (!this.file.type.includes('image')) {
      this.errors.push('File not an image');
    }
    if (this.file.size > this.maxFileSize) {
      this.errors.push(`File is too big. Maximum file size: ${this.maxFileSize / 1024 / 1024} MB`);
    }

    if (this.errors.length > 0) {
      this.file = null;
    } else {
      this.onFileSelected.emit(this.file);
    }
  }
}
