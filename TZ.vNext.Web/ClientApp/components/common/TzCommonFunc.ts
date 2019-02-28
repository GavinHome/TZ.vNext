
import "../extension/StringExtensions"
import StoreCache from './TzStoreCache'
import IUrlParameterSchema from "../schemas/IUrlParameterSchema";

export default function AuthFunction(value: any): boolean {
    let cache = new StoreCache('auth')
    let functions = cache.get('functions') as string
    let function_array = String.isNullOrEmpty(functions) ? [] : functions.split(',')
    return !value.isHidden && (value.meta.functionId == null || function_array && function_array.filter(x => x === value.meta.functionId).length > 0)
}

export function encodeQueryData(data: IUrlParameterSchema) {
    let ret: string[] = [];
    if (data) {
        for (let d in data) {
            if (d && data[d] !== undefined) {
                ret.push(encodeURIComponent(d) + '=' + encodeURIComponent(data[d].toString()))
            }
        }
    }
    
    return ret.join('&');
}