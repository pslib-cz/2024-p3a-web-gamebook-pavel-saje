import { useState } from "react";
import React from "react";

interface DropdownProps {
    title: string;
    children: React.ReactNode;
}

const Dropdown: React.FC<DropdownProps> = ({ title, children }) => {
    const [isVisible, setIsVisible] = useState(false);

    return (
        <>
            <a
                className={
                    isVisible
                        ? "dropdown-text--opened"
                        : "dropdown-text--closed"
                }
                onClick={() => setIsVisible((prev) => !prev)}
            >
                {title}
            </a>
            <div
                className={`dropdown ${
                    isVisible ? "dropdown--visible" : "dropdown--hidden"
                }`}
            >
                {React.Children.map(children, (child) => {
                    if (React.isValidElement(child)) {
                        return React.cloneElement(child, {
                            className: "dropdown__item",
                        });
                    }
                    return child;
                })}
            </div>
        </>
    );
};

export default Dropdown;
