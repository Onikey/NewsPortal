import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TinymceComponent } from './tinymce.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TinymceOptions } from './tinymce.config.interface';
import { TinymceDefaultOptions } from './tinymce.default';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [
    TinymceComponent
  ],
  exports: [
    TinymceComponent
  ],
  providers: [
    { provide: 'TINYMCE_CONFIG', useClass: TinymceDefaultOptions }
  ]
})
export class TinymceModule {
  static withConfig(userConfig: TinymceOptions = {}): ModuleWithProviders {
    return {
      ngModule: TinymceModule,
      providers: [
        { provide: 'TINYMCE_CONFIG', useValue: userConfig }
      ]
    }
  }
}
