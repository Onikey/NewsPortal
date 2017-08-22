export class ToastrOptions {
    /**
     * @default false
     */
    public closeButton: boolean = false;

    /**
     * @default false
     */
    public debug: boolean = false;

    /**
     * @default true
     */
    public newestOnTop: boolean = true;

    /**
     * @default false
     */
    public progressBar: boolean = false;

    /**
     * @default false
     */
    public preventDuplicates: boolean = false;

    /**
     * @default null
     */
    public onclick: Function = null;

    /**
     * @default 300
     */
    public showDuration: number = 300;

    /**
     * @default 1000
     */
    public hideDuration: number = 1000;

    /**
     * @default 5000
     */
    public timeOut: number = 5000;

    /**
     * @default 1000
     */
    public extendedTimeOut: number = 1000;

    /**
     * @default 'swing'
     */
    public showEasing = 'swing';

    /**
     * @default 'linear'
     */
    public hideEasing = 'linear';

    /**
     * @default 'fadeIn'
     */
    public showMethod = 'fadeIn';

    /**
     * @default 'fadeOut'
     */
    public hideMethod = 'fadeOut';

    private _positionClass: string;

    /**
     * @default 'toast-top-right'
     */
    public get positionClass(): string {
        return this._positionClass;
    }

    /**
     * @default TopRight
     */
    public set position(value: ToastrPosition) {
        let result: string;

        switch (value) {
            case ToastrPosition.TopRight:
                result = 'toast-top-right';
                break;
            case ToastrPosition.BottomRight:
                result = 'toast-bottom-right';
                break;
            case ToastrPosition.BottomLeft:
                result = 'toast-bottom-left';
                break;
            case ToastrPosition.TopLeft:
                result = 'toast-top-left';
                break;
            case ToastrPosition.TopFullWidth:
                result = 'toast-top-full-width';
                break;
            case ToastrPosition.BottomFullWidth:
                result = 'toast-bottom-full-width';
                break;
            case ToastrPosition.TopCenter:
                result = 'toast-top-center';
                break;
            case ToastrPosition.BottomCenter:
                result = 'toast-bottom-center';
                break;
            default:
                result = '';
                break;
        }

        this._positionClass = result;
    }

    constructor() {
        this.position = ToastrPosition.TopRight;
    }
}

export enum ToastrPosition {
    TopRight,
    BottomRight,
    BottomLeft,
    TopLeft,
    TopFullWidth,
    BottomFullWidth,
    TopCenter,
    BottomCenter
}
