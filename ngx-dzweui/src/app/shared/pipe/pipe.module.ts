import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HostUrlPipe } from './host-url.pipe';
import { KeyHighlightPipe } from './key-highlight.pipe';
import { LengthLimitPipe } from './length-limit.pipe';

@NgModule({
    imports: [CommonModule],
    exports: [HostUrlPipe,
        KeyHighlightPipe,
        LengthLimitPipe,
    ],
    declarations: [
        HostUrlPipe,
        KeyHighlightPipe,
        LengthLimitPipe,
    ],
    providers: [
    ],
})
export class PipeModule { }
