const MenuIcons = [
    {
        key: "Create",
        value: "fa fa-file"
    },
    {
        key: "Detail",
        value: "fa fa-eye"
    },
    {
        key: "Edit",
        value: "fa fa-pencil"
    },
    {
        key: "Delete",
        value: "fa fa-remove"
    },
    {
        key: "Audit",
        value: "fa fa-check"
    },
    {
        key: "Enable",
        value: "fa fa-check"
    },
    {
        key: "Disable",
        value: "fa fa-ban"
    },
    {
        key: "Termination",
        value: "fa fa-stop-circle-o "
    },
    {
        key: "ReportGenerate",
        value: "fa fa-hand-pointer-o"
    },
    {
        key: "ReportView",
        value: "fa fa-list-alt"
    }
];

interface CommandInfo {
    name: string;
    title: string;
    action: any;
    visible: any;
}

export class GridCommon {
    public bindCommands(commands: Array<CommandInfo>) {
        var result = commands.map((item, index) =>
            Object({
                name: item.name,
                text: '',
                title: item.title,
                className: "c-grid-menu c-grid-menu--mini c-grid-menu-" + item.name,
                ////template: "<a role='button' title=" + item.title + " class='k-button k-grid-"+ item.name + "' href='##'><span class='"+ MenuIcons.filter(x => x.key === item.name.toString())[0].value + "'></span>"+ item.title +"</a>",
                click: item.action,
                visible: item.visible,
                iconClass: MenuIcons.filter(x => x.key === item.name.toString())[0] ? MenuIcons.filter(x => x.key === item.name.toString())[0].value : ''
            })
        )

        return result;
    }
}