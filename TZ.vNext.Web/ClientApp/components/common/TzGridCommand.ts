export interface GridCommand {
    name: string;
    title: string;
    route: string;
    api: string;
    action: (e?: any) => void;
    visible: (dataItem: any) => boolean;
    param?: any;
    is?: boolean;
    index: number;
}
