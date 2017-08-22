import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import * as stringDecoder from 'string_decoder';

declare var $: any;

@Component({
    selector: 'app-delete-modal',
    template: `
        <div id="DeleteModal" class="ui mini modal">
            <div class="header">
            Delete {{options.itemName}}
            </div>
            <div class="content">
            <p>Are you sure you want to delete {{options.itemName}}</p>
            </div>
            <div class="actions">
            <div class="ui negative button">No</div>
            <div class="ui positive right labeled icon button">Yes<i class="checkmark icon"></i>
            </div>
            </div>
        </div>
    `
})
export class DeleteModalComponent implements OnInit, OnDestroy {
    @Input() options: DeleteModalOptions;
    private deleteModal = null;


    constructor() { }

    ngOnInit() {
        const options = this.options;
        this.deleteModal = $('#DeleteModal')
            .modal({
                closable: options.closable,
                onDeny: options.onDeny,
                onApprove: options.onApprove,
                onHidden: options.onHidden
            }).modal('show');
    }

    ngOnDestroy() {
        this.deleteModal.modal('hide');
    }


}

export class DeleteModalOptions {
    itemName: string;
    /**
     *  @default true
     */
    closable?: boolean = true;
    onDeny?: Function;
    onApprove?: Function;
    onHidden?: Function;
}
