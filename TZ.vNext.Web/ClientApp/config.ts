export interface ConfigInfo {
    CaBaseUrl: string;
    SalaryUrl: string;
    EnableCA: boolean;
}

let config: ConfigInfo = {
    CaBaseUrl: "http://a.com:7779",
    SalaryUrl: "http://localhost:27249",
    EnableCA: false
}

export default config