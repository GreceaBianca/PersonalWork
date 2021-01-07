import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ColorPickerModule } from 'ngx-color-picker';
import { FileUploadModule } from 'ng2-file-upload';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { CarouselModule } from 'ngx-bootstrap/carousel';

import { FilterNamePipe } from './pipes/filterStrings.pipe';
import { FileUploaderComponent } from './components/file-uploader/file-uploader.component';


@NgModule({
  imports: [
    ModalModule,
    TabsModule.forRoot(),
    BrowserAnimationsModule,
    ColorPickerModule,
    TypeaheadModule.forRoot(),
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    FileUploadModule
  ],
  declarations: [
    FilterNamePipe,
    FileUploaderComponent
  ],
  exports: [
    ModalModule,
    TabsModule,
    BrowserAnimationsModule,
    TypeaheadModule,
    BsDropdownModule,
    BsDatepickerModule,
    CollapseModule,
    TooltipModule,
    ColorPickerModule,
    FilterNamePipe,
    FileUploadModule,
    FileUploaderComponent,
  ]
})
export class SharedModule { }
