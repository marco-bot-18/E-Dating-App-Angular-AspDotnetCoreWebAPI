import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';

import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgxGalleryModule,
    NgxSpinnerModule.forRoot({
      type: 'line-spin-clockwise-fade',
    }),
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true, 
    }),
  ],
  exports: [
    NgxGalleryModule,
    NgxSpinnerModule,
    BsDropdownModule,
    TabsModule,
    ToastrModule,
  ],
})
export class SharedModule {}
