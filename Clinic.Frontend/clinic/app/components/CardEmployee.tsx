interface Props{
    name: string;
}

export const CardEmployee = ({name}: Props) => {
    return (
        <div style = {{
            display: "flex",
            flexDirection: "row",
            alignItems: "center",
            justifyContent: "space-between"
        }}>
            <p className="card_name">{name}</p>
        </div>
    )
}
